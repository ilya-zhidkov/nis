using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels;

public class TestingViewModel : Conductor<Screen>
{
    private readonly SimpleContainer _container;

    public TestingViewModel(SimpleContainer container)
    {
        _container = container;
    }
    //Task.Run(async () => await ActivateItemAsync(_container.GetInstance<TestComboViewModel>()));

    public void btn1() => ActivateItemAsync(_container.GetInstance<AdlViewModel>());

    public void btn2() => ActivateItemAsync(_container.GetInstance<DekubitViewModel>());

}
