﻿using AutoMapper;
using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using ScaleType = Nis.Core.Models.Enums.ScaleType;

namespace Nis.WpfApp.ViewModels;

public class ActivityViewModel : Screen
{
    private Form _form;
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<MedicalScale> _scales;

    public BindableCollection<MedicalScale> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            NotifyOfPropertyChange(() => Scales);
        }
    }

    public ActivityViewModel(
        IMapper mapper,
        DataContext context,
        SimpleContainer container,
        IEventAggregator aggregator
    )
    {
        _mapper = mapper;
        _context = context;
        _container = container;
        _container = container;
        _aggregator = aggregator;
    }

    public async Task Activity() => await _aggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus()
    {
        var scales = new List<MedicalScale>();

        foreach (var scale in _scales.Where(scale => scale.Activities.Any(activity => activity.IsChecked)))
        {
            var activities = new BindableCollection<MedicalScaleActivity>();

            foreach (var activity in scale.Activities.Where(activity => activity.IsChecked))
                activities.Add(activity);

            scales.Add(new MedicalScale { Name = scale.Name, ScaleType = WpfApp.Models.ScaleType.Activity, Activities = activities });
        }

        _form.Scales = scales;

        await _aggregator.PublishOnUIThreadAsync("Decubitus");
    }

    public async Task Malnutrition() => await _aggregator.PublishOnUIThreadAsync("Malnutrition");

    public async Task Fall() => await _aggregator.PublishOnUIThreadAsync("Fall");

    protected override async void OnViewLoaded(object view)
    {
        _form = _container.GetInstance<Form>();

        Scales = _mapper.Map<BindableCollection<MedicalScale>>(
            await _context.Scales
                .Include(scale => scale.Activities)
                .Where(scale => scale.ScaleType == ScaleType.Activity)
                .ToListAsync()
        );
    }
}
