using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using System.Windows.Threading;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Nis.WpfApp.ViewModels
{
    internal class FallViewModel : Screen
    {
        private readonly DataContext _context;
        private readonly IWindowManager _window;
        private readonly SimpleContainer _container;
        private readonly IEventAggregator _events;
        public FallViewModel(DataContext context, SimpleContainer container, IWindowManager window, IEventAggregator events)
        {
            _window = window;
            _context = context;
            _container = container;
            _events = events;
        }
        public async Task Adl()
        {
            await _events.PublishOnUIThreadAsync("ADL");
        }

        public async Task Dekubit()
        {
            await _events.PublishOnUIThreadAsync("Dekubit");
        }

        public async Task Malnutrice()
        {
            await _events.PublishOnUIThreadAsync("Malnutrice");
        }

        public async Task Fall()
        {
            await _events.PublishOnUIThreadAsync("Fall");
        }

        public async Task Submit()
        {
            MessageBox.Show("Odesláno");
        }
    }
}
