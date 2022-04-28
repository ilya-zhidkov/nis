using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels
{
    class MainViewModel : Conductor<object>
    {

        private readonly SimpleContainer _container;

        public string tst0 { get; set; }

        public MainViewModel(SimpleContainer container)
        {
            _container = container;
            ActivateItem(_container.GetInstance<PatientViewModel>());
        }

    }
}
