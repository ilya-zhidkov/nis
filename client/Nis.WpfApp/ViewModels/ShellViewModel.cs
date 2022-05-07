using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private readonly SimpleContainer _container;

    public string tst0 { get; set; }

    public ShellViewModel(SimpleContainer container)
    {
        _container = container;

        Task.Run(async () => await ActivateItemAsync(_container.GetInstance<PatientViewModel>()));
    }

    public void btn_tst() => ActivateItemAsync(_container.GetInstance<TestComboViewModel>());

    public void btn_pac() => ActivateItemAsync(_container.GetInstance<PatientViewModel>());
}
