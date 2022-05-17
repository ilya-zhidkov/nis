using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels;

public class TestingViewModel : Conductor<Screen>
{
    private readonly SimpleContainer _container;

    public TestingViewModel(SimpleContainer container) => _container = container;

    public void btn1() => ActivateItemAsync(_container.GetInstance<ActivityViewModel>());

    public void btn2() => ActivateItemAsync(_container.GetInstance<DecubitusViewModel>());
}
