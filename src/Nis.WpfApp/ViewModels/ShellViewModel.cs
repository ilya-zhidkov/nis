﻿using Caliburn.Micro;

namespace Nis.WpfApp.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly SimpleContainer _container;

        public string tst0 { get; set; }

        public ShellViewModel(SimpleContainer container)
        {
            _container = container;
            ActivateItem(_container.GetInstance<PatientViewModel>());
        }
    }
}
