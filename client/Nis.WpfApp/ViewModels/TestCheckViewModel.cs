using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels;

public class TestCheckViewModel : Conductor<object>
{
    private readonly SimpleContainer _container;

    public TestCheckViewModel()
    {
        //ActivateItem(_container.GetInstance<PatientViewModel>());
    }
}
