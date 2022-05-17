using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels;

public class MalnutritionViewModel : Screen
{
    private readonly IEventAggregator _eventAggregator;

    public MalnutritionViewModel(IEventAggregator eventAggregator) => _eventAggregator = eventAggregator;

    public async Task Activity() => await _eventAggregator.PublishOnUIThreadAsync("Activity");

    public async Task Decubitus() => await _eventAggregator.PublishOnUIThreadAsync("Decubitus");

    public async Task Malnutrition() => await _eventAggregator.PublishOnUIThreadAsync("Malnutrition");

    public async Task Fall() => await _eventAggregator.PublishOnUIThreadAsync("Fall");
}
