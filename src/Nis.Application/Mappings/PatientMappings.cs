using AutoMapper;
using Nis.Core.Models;
using Nis.Application.DTOs;

namespace Nis.Application.Mappings
{
    public class PatientMappings  : Profile
    {
        public PatientMappings()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
        }
    }
}
