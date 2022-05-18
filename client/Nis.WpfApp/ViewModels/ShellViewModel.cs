using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<string>
{
    private string _username;
    private string _profileImage;
    private readonly Student _student;
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

    public string Username
    {
        get => $"{_student.FirstName} {_student.LastName}";
        set
        {
            _username = value;
            NotifyOfPropertyChange(() => Username);
        }
    }

    public string ProfileImage
    {
        get => _student.ProfileImage;
        set
        {
            _profileImage = value;
            NotifyOfPropertyChange(() => ProfileImage);
        }
    }

    public void ShutdownApplication() => TryCloseAsync();

    public void StartExam() => ActivateItemAsync(_container.GetInstance<ExamPickViewModel>());

    public Task HandleAsync(string message, CancellationToken cancellationToken) => Task.FromResult(message switch
    {
        "Exam" => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>(), cancellationToken),
        "Activity" => ActivateItemAsync(_container.GetInstance<ActivityViewModel>(), cancellationToken),
        "Decubitus" => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>(), cancellationToken),
        "Malnutrition" => ActivateItemAsync(_container.GetInstance<MalnutritionViewModel>(), cancellationToken),
        "Fall" => ActivateItemAsync(_container.GetInstance<FallViewModel>(), cancellationToken),
        _ => throw new Exception("Unknown Message")
    });
}
