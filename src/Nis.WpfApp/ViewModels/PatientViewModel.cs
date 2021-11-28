﻿using System.Linq;
using Caliburn.Micro;
using Nis.WpfApp.Models;
using Nis.Core.Persistence;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels
{
    public class PatientViewModel : Screen
    {
        private readonly DataContext _context;
        private BindingList<Patient> _patients;

        public BindingList<Patient> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                NotifyOfPropertyChange(() => Patients);
            }
        }

        public PatientViewModel(DataContext context) => _context = context;

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await FetchPatientsAsync();
        }

        private async Task FetchPatientsAsync()
        {
            Patients = new BindingList<Patient>((await _context.Patients.ToListAsync())
                .Select(patient => new Patient
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName
                })
                .ToList());
        }
    }
}
