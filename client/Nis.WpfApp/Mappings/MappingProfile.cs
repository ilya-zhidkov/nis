using AutoMapper;
using Domain = Nis.Core.Models;
using Client = Nis.WpfApp.Models;

namespace Nis.WpfApp.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.MedicalScales.ScaleActivity, Client.MedicalScaleActivity>()
            .ForMember(activity => activity.IsChecked, options => options.Ignore())
            .ForMember(activity => activity.IsNotifying, options => options.Ignore());

        CreateMap<Domain.MedicalScales.Scale, Client.MedicalScale>()
            .ForMember(scale => scale.ScaleType, options => options.MapFrom(scale => scale.ScaleType));
    }
}

