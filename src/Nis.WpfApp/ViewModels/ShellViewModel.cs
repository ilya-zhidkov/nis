using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;

namespace Nis.WpfApp.ViewModels
{
    class ShellViewModel : Conductor<object>
    {
        private readonly SimpleContainer _container;

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
        }

        public void btn_tst() {
            ActivateItem(_container.GetInstance<TestComboViewModel>());
        }

        public void btn_pac()
        {
            ActivateItem(_container.GetInstance<PatientViewModel>());
        }

    }
}
