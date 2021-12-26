using AutoMapper;
using Caliburn.Micro;
using Nis.Application.Mappings;

namespace Nis.Application
{
    public static class ApplicationServiceRegistration
    {
        public static SimpleContainer AddMappings(this SimpleContainer container)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PatientMappings());
            });
            
            var mapper = configuration.CreateMapper();
            
            return container.Instance(mapper);
        }
    }
}
