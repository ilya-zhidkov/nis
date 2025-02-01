using AutoMapper;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using ScaleType = Nis.Core.Models.Enums.ScaleType;

namespace Nis.WpfApp.ViewModels;

[UsedImplicitly]
public class DecubitusViewModel(
    IMapper mapper,
    DataContext context,
    SimpleContainer container,
    IEventAggregator aggregator
) : Screen, IHandle<MedicalScaleActivity>
{
    private byte _points;
    private Form _form = null!;
    private BindableCollection<MedicalScale> _scales = [];

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

    public async Task Activity() => await aggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus() => await aggregator.PublishOnUIThreadAsync("Decubitus");

    public async Task Malnutrition()
    {
        var scales = new List<MedicalScale>();

        foreach (var scale in _scales.Where(scale => scale.Activities.Any(activity => activity.IsChecked)))
        {
            var activities = new BindableCollection<MedicalScaleActivity>();

            foreach (var activity in scale.Activities.Where(activity => activity.IsChecked))
                activities.Add(activity);

            scales.Add(new() { Name = scale.Name, ScaleType = WpfApp.Models.ScaleType.Decubitus, Activities = activities });
        }

        _form.Scales = _form.Scales.Concat(scales);

        await aggregator.PublishOnUIThreadAsync("Malnutrition");
    }

    public async Task FallAsync() => await aggregator.PublishOnUIThreadAsync("Fall");

    public async Task HandleAsync(MedicalScaleActivity message, CancellationToken cancellation)
    {
        var (_, score, isChecked) = message;

        Points = isChecked ? Points += score : Points -= score;

        await Task.FromResult(Points);
    }

    protected override async Task<Task> OnInitializeAsync(CancellationToken cancellation)
    {
        Scales = mapper.Map<BindableCollection<MedicalScale>>(
            await context.Scales
                .Include(scale => scale.Activities)
                .Where(scale => scale.ScaleType == ScaleType.Decubitus)
                .ToListAsync(cancellation)
        );

        return base.OnInitializeAsync(cancellation);
    }

    protected override void OnViewLoaded(object view) => _form = container.GetInstance<Form>();

    protected override Task OnActivateAsync(CancellationToken cancellation)
    {
        aggregator.SubscribeOnPublishedThread(this);

        return base.OnActivateAsync(cancellation);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellation)
    {
        aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellation);
    }
}
