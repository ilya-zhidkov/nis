using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<string>
{
    private Student _student;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _eventAggregator;
    private string _backCol;

    public ShellViewModel(
        SimpleContainer container,
        Student student,
        IEventAggregator eventAggregator
    )
    {
        BackCol = "White";
        _student = student;
        _container = container;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnPublishedThread(this);
        ActivateItemAsync(_container.GetInstance<InstructionsViewModel>());
    }

    public string BackCol
    {
        get => _backCol;
        set
        {
            _backCol = value;
            NotifyOfPropertyChange(() => BackCol);
        }
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

    public void StartExam() => ActivateItemAsync(_container.GetInstance<ExamPickViewModel>());
    public void OpenInstructions() => ActivateItemAsync(_container.GetInstance<InstructionsViewModel>());
    public void OpenSettings() => ActivateItemAsync(_container.GetInstance<OpenSettingsViewModel>());
    public Task HandleAsync(string message, CancellationToken cancellationToken) => Task.FromResult(message switch
    {
        "Exam" => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>(), cancellationToken),
        "Activity" => ActivateItemAsync(_container.GetInstance<ActivityViewModel>(), cancellationToken),
        "Decubitus" => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>(), cancellationToken),
        "Malnutrition" => ActivateItemAsync(_container.GetInstance<MalnutritionViewModel>(), cancellationToken),
        "Fall" => ActivateItemAsync(_container.GetInstance<FallViewModel>(), cancellationToken),
        "Instructions" => ActivateItemAsync(_container.GetInstance<InstructionsViewModel>(), cancellationToken),
        _ => throw new Exception("Unknown Message")
    });
}
