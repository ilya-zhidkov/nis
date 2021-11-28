using System.Linq;
using Caliburn.Micro;
using Nis.Core.Extensions;
using Nis.Core.Persistence;

namespace Nis.WpfApp.Extensions
{
    public static class SimpleContainerExtensions
    {
        public static SimpleContainer AddDatabase(this SimpleContainer container, string connectionString)
        {
            container.Instance(new DataContext(DatabaseExtensions.ConnectToDatabase(connectionString)));

            return container;
        }

        public static SimpleContainer AddViewModels(this SimpleContainer container)
        {
            typeof(Bootstrapper).Assembly.GetTypes()
                .Where(type => type.IsClass && type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(vm => container.RegisterPerRequest(
                    service: vm,
                    key: vm.ToString(),
                    implementation: vm
                ));

            return container;
        }
    }
}
