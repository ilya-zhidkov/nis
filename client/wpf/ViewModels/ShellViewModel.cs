using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<string>
{
    private Student _student;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;
    private string _backCol;

    public ShellViewModel(
        SimpleContainer container,
        Student student,
        IEventAggregator aggregator
    )
    {
        BackCol = "White";
        _student = student;
        _container = container;
        _aggregator = aggregator;
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

    public void StartExam() => ActivateItemAsync(_container.GetInstance<AnamnesesViewModel>());

    public void OpenSettings() => ActivateItemAsync(_container.GetInstance<OpenSettingsViewModel>());

    public async Task HandleAsync(string message, CancellationToken cancellationToken) => await Task.FromResult(message switch
    {
        "Exam" => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>(), cancellationToken),
        "Activity" => ActivateItemAsync(_container.GetInstance<ActivityViewModel>(), cancellationToken),
        "Decubitus" => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>(), cancellationToken),
        "Malnutrition" => ActivateItemAsync(_container.GetInstance<MalnutritionViewModel>(), cancellationToken),
        "Fall" => ActivateItemAsync(_container.GetInstance<FallViewModel>(), cancellationToken),
        _ => throw new Exception("Unknown Message")
    });

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _aggregator.SubscribeOnPublishedThread(this);

        ActivateItemAsync(_container.GetInstance<AnamnesesViewModel>(), cancellationToken);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        _aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellationToken);
    }
}
