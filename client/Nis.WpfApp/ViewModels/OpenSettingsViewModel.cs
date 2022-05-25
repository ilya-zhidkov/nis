using Caliburn.Micro;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.ViewModels
{
    internal class OpenSettingsViewModel
    {
        private readonly SimpleContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public OpenSettingsViewModel(
            SimpleContainer container,
            IEventAggregator eventAggregator
        )
        {
            _container = container;
            _eventAggregator = eventAggregator;
        }

        public async Task Green() => await _eventAggregator.PublishOnUIThreadAsync("Green");
        public async Task Red() => await _eventAggregator.PublishOnUIThreadAsync("Red");
        public async Task White() => await _eventAggregator.PublishOnUIThreadAsync("White");
    }
}
