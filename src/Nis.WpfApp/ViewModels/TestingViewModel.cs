using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels
{
    class TestingViewModel : Conductor<object>
    {
        private readonly SimpleContainer _container;

        public TestingViewModel(SimpleContainer container)
        {
            _container = container;
            ActivateItem(_container.GetInstance<TestComboViewModel>());
        }

    }
}
