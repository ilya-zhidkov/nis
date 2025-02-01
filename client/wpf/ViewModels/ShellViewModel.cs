using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>, IHandle<string>
{
    private Student? _student;
    private string _backCol = null!;
    private readonly SimpleContainer _container;
    private readonly IEventAggregator _aggregator;

    public ShellViewModel(
        SimpleContainer container,
        Student? student,
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

    public Student? Student
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

    public async Task HandleAsync(string message, CancellationToken cancellation) => await Task.FromResult(message switch
    {
        "Exam" => ActivateItemAsync(_container.GetInstance<PatientClassificationViewModel>(), cancellation),
        "Activity" => ActivateItemAsync(_container.GetInstance<ActivityViewModel>(), cancellation),
        "Decubitus" => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>(), cancellation),
        "Malnutrition" => ActivateItemAsync(_container.GetInstance<MalnutritionViewModel>(), cancellation),
        "Fall" => ActivateItemAsync(_container.GetInstance<FallViewModel>(), cancellation),
        _ => throw new("Unknown Message")
    });

    protected override Task OnActivateAsync(CancellationToken cancellation)
    {
        _aggregator.SubscribeOnPublishedThread(this);

        ActivateItemAsync(_container.GetInstance<AnamnesesViewModel>(), cancellation);

        return base.OnActivateAsync(cancellation);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellation)
    {
        _aggregator.Unsubscribe(this);

        return base.OnDeactivateAsync(close, cancellation);
    }
}
