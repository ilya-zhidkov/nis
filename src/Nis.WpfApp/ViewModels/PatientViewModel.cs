using AutoMapper;
using Caliburn.Micro;
using Nis.Application.DTOs;
using Nis.Core.Persistence;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels
{
    public class PatientViewModel : Screen
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private BindingList<PatientDto> _patients;

        public BindingList<PatientDto> Patients
        {
            get => _patients;
            set
            {
                _patients = value;
                NotifyOfPropertyChange(() => Patients);
            }
        }

        public PatientViewModel(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await FetchPatientsAsync();
        }

        private async Task FetchPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            var patientsDto = _mapper.Map<List<PatientDto>>(patients);
            Patients = new BindingList<PatientDto>(patientsDto);
        }
    }
}
