using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Nis.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConnectToDatabase(this IServiceCollection services, string connectionString)
        {
            services?.AddDbContext<DataContext>(options => options.UseSqlite(connectionString));

            return services;
        }
    }
}
