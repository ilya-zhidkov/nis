using Caliburn.Micro;
using Nis.WpfApp.Models;
using System.Windows;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<Screen>, IHandle<string>
{
    private string _username;
    private readonly Student _student;
    private readonly IEventAggregator _events;
    private readonly SimpleContainer _container;


    public ShellViewModel(SimpleContainer container, Student student, IEventAggregator events)
    {
        _student = student;
        _container = container;
        _events = events;
        _events.SubscribeOnUIThread(this);
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

    public void StartExam() 
    { 
        ActivateItemAsync(_container.GetInstance<TestComboViewModel>());
    }

    public void ShowExam() => ActivateItemAsync(_container.GetInstance<TestCheckViewModel>());

    public void btn_pac() => ActivateItemAsync(_container.GetInstance<PatientViewModel>());

    public Task HandleAsync(string message, CancellationToken cancellationToken)
    {
        //MessageBox.Show(message);
        if (message == "ADL")
        {
            return ActivateItemAsync(_container.GetInstance<AdlViewModel>());
        }
        else if (message == "Dekubit")
        {
            return ActivateItemAsync(_container.GetInstance<DekubitViewModel>());
        }
        else if(message == "Malnutrice")
        {
            return ActivateItemAsync(_container.GetInstance<MalnutriceViewModel>());
        }
        else if(message == "Fall")
        {
            return ActivateItemAsync(_container.GetInstance<FallViewModel>());
        }
        else
        {
            throw new Exception("Unknown Message");
        }
    }
}
