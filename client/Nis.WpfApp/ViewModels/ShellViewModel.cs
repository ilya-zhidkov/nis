using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.WpfApp.Messages;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>
    , IHandle<MedicalScaleMessage>
{
    private string _username;
    private readonly Student _student;
    private readonly IEventAggregator _eventAggregator;
    private readonly SimpleContainer _container;

    public ShellViewModel(SimpleContainer container, Student student, IEventAggregator eventAggregator)
    {
        _student = student;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnPublishedThread(this);
        _container = container;
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

    public void StartExam() => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>());

    public Task HandleAsync(MedicalScaleMessage message, CancellationToken cancellationToken)
        =>
            ActivateItemAsync(_container.GetInstance<MedicalScaleViewModel>(), cancellationToken);
}