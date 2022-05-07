using AutoMapper;
using Domain = Nis.Core.Models;
using Client = Nis.WpfApp.Models;

namespace Nis.WpfApp.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile() => CreateMap<Domain.Patient, Client.Patient>();
}

