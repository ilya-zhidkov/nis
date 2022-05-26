using AutoMapper;
using Caliburn.Micro;
using Notification.Wpf;
using Nis.WpfApp.Models;
using Nis.WpfApp.Requests;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using ScaleType = Nis.Core.Models.Enums.ScaleType;

namespace Nis.WpfApp.ViewModels;

public class FallViewModel : Screen
{
    private Form _form;
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly UploadRequest _request;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;
    private BindableCollection<MedicalScale> _scales;
    private readonly INotificationManager _notifications;

    public BindableCollection<MedicalScale> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            NotifyOfPropertyChange(() => Scales);
        }
    }

    public FallViewModel(
        IMapper mapper,
        DataContext context,
        UploadRequest request,
        SimpleContainer container,
        IEventAggregator aggregator,
        INotificationManager notifications
    )
    {
        _mapper = mapper;
        _context = context;
        _request = request;
        _container = container;
        _aggregator = aggregator;
        _notifications = notifications;
    }

    public async Task Activity() => await _aggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus() => await _aggregator.PublishOnUIThreadAsync("Decubitus");

    public async Task Malnutrition() => await _aggregator.PublishOnUIThreadAsync("Malnutrition");

    public async Task Fall() => await _aggregator.PublishOnUIThreadAsync("Fall");

    public async Task SubmitAsync()
    {
        var scales = new List<MedicalScale>();

        foreach (var scale in _scales.Where(scale => scale.Activities.Any(activity => activity.IsChecked)))
        {
            var activities = new BindableCollection<MedicalScaleActivity>();

            foreach (var activity in scale.Activities.Where(activity => activity.IsChecked))
                activities.Add(activity);

            scales.Add(new MedicalScale { Name = scale.Name, ScaleType = WpfApp.Models.ScaleType.RiskOfFall, Activities = activities });
        }

        _form.Scales = _form.Scales.Concat(scales);

        _notifications.Show("Test vyplněn!", "Vaše výsledky byly odeslány k vyhodnocení.", NotificationType.Success, "WindowArea");

        await _request.UploadAsync(_form);
    }

    protected override async void OnViewLoaded(object view)
    {
        _form = _container.GetInstance<Form>();

        Scales = _mapper.Map<BindableCollection<MedicalScale>>(
            await _context.Scales
                .Include(scale => scale.Activities)
                .Where(scale => scale.ScaleType == ScaleType.RiskOfFall)
                .ToListAsync()
        );
    }
}
