﻿using AutoMapper;
using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using ScaleType = Nis.Core.Models.Enums.ScaleType;

namespace Nis.WpfApp.ViewModels;

public class DecubitusViewModel : Screen, IHandle<MedicalScaleActivity>
{
    private Form _form;
    private byte _points;
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<MedicalScale> _scales;

    public byte Points
    {
        get => _points;
        set
        {
            _points = value;
            NotifyOfPropertyChange(() => Points);
        }
    }

    public BindableCollection<MedicalScale> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            NotifyOfPropertyChange(() => Scales);
        }
    }

    public DecubitusViewModel(
        IMapper mapper,
        DataContext context,
        SimpleContainer container,
        IEventAggregator aggregator
    )
    {
        _mapper = mapper;
        _context = context;
        _container = container;
        _aggregator = aggregator;
    }

    public async Task Activity() => await _aggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus() => await _aggregator.PublishOnUIThreadAsync("Decubitus");

    public async Task Malnutrition()
    {
        var scales = new List<MedicalScale>();

        foreach (var scale in _scales.Where(scale => scale.Activities.Any(activity => activity.IsChecked)))
        {
            var activities = new BindableCollection<MedicalScaleActivity>();

            foreach (var activity in scale.Activities.Where(activity => activity.IsChecked))
                activities.Add(activity);

            scales.Add(new MedicalScale { Name = scale.Name, ScaleType = WpfApp.Models.ScaleType.Decubitus, Activities = activities });
        }

        _form.Scales = _form.Scales.Concat(scales);

        await _aggregator.PublishOnUIThreadAsync("Malnutrition");
    }

    public async Task Fall() => await _aggregator.PublishOnUIThreadAsync("Fall");
    
    protected override async void OnViewLoaded(object view)
    {
        _form = _container.GetInstance<Form>();

        Scales = _mapper.Map<BindableCollection<MedicalScale>>(
            await _context.Scales
                .Include(scale => scale.Activities)
                .Where(scale => scale.ScaleType == ScaleType.Decubitus)
                .ToListAsync()
        );
    }

    public async Task HandleAsync(MedicalScaleActivity message, CancellationToken cancellationToken)
    {
        var (_, score, isChecked) = message;

        Points = isChecked ? Points += score : Points -= score;

        await Task.FromResult(Points);
    }

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _aggregator.SubscribeOnPublishedThread(this);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        _aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellationToken);
    }
}
