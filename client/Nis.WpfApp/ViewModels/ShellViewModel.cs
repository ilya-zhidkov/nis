using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<string>
{
    private Student _student;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _eventAggregator;

    public ShellViewModel(
        SimpleContainer container,
        Student student,
        IEventAggregator eventAggregator
    )
    {
        _student = student;
        _container = container;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnPublishedThread(this);
    }

    public Student Student
    {
        get => _student;
        set
        {
            _student = value;
            NotifyOfPropertyChange(() => Student);
        }
    }

    public void ShutdownApplication() => TryCloseAsync();

    public void StartExam() => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>());

    public Task HandleAsync(string message, CancellationToken cancellationToken) => Task.FromResult(message switch
    {
        "Activity" => ActivateItemAsync(_container.GetInstance<ActivityViewModel>(), cancellationToken),
        "Decubitus" => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>(), cancellationToken),
        "Malnutrition" => ActivateItemAsync(_container.GetInstance<MalnutritionViewModel>(), cancellationToken),
        "Fall" => ActivateItemAsync(_container.GetInstance<FallViewModel>(), cancellationToken),
        _ => throw new Exception("Unknown Message")
    });
}
