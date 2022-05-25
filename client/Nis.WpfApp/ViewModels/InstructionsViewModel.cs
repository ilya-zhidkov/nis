using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels
{
    internal class InstructionsViewModel : Screen
    {
        private readonly SimpleContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public InstructionsViewModel(
            SimpleContainer container,
            IEventAggregator eventAggregator
        )
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }
    }
}
