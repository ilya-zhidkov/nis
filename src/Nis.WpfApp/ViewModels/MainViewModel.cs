using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels
{
    class MainViewModel : Conductor<object>
    {

        private readonly SimpleContainer _container;

        public MainViewModel(SimpleContainer container)
        {
            _container = container;
            ActivateItem(_container.GetInstance<PatientViewModel>());
        }

    }
}
