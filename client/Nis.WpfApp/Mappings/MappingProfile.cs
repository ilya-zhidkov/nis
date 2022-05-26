using AutoMapper;
using Domain = Nis.Core.Models;
using Client = Nis.WpfApp.Models;

namespace Nis.WpfApp.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Patient, Client.Patient>();
        CreateMap<Domain.MedicalScales.ScaleActivity, Client.MedicalScaleActivity>();
        CreateMap<Domain.MedicalScales.Scale, Client.MedicalScale>()
            .ForMember(scale => scale.ScaleType, options => options.MapFrom(scale => scale.ScaleType));
    }
}

