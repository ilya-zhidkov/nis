using Nis.Core.Extensions;
using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Nis.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options => options.UseSqlite(DatabaseExtensions.ConnectionString));

        return services;
    }
}
