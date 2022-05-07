using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels;

public class TestingViewModel : Conductor<object>
{
    private readonly SimpleContainer _container;

    public TestingViewModel(SimpleContainer container)
    {
        _container = container;

        Task.Run(async () => await ActivateItemAsync(_container.GetInstance<TestComboViewModel>()));
    }
}
