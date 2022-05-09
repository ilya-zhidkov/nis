using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private string _username;
    private readonly Student _student;
    private readonly IEventAggregator _events;
    private readonly SimpleContainer _container;

    public string tst0 { get; set; }

    public ShellViewModel(SimpleContainer container, Student student)
    {
        _student = student;
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

    public void btn_tst() => ActivateItemAsync(_container.GetInstance<TestComboViewModel>());

    public void btn_pac() => ActivateItemAsync(_container.GetInstance<PatientViewModel>());
}
