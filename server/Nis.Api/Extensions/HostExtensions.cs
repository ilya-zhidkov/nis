using Nis.Core.Persistence;
using Nis.Core.Persistence.Seeders;

namespace Nis.Api.Extensions;

public static class HostExtensions
{
    public static IHost Seed(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.EnsureCreated();
            new DepartmentSeeder().Seed(context);
            new DiagnosisSeeder().Seed(context);
            new DietSeeder().Seed(context);
            new ExamSeeder().Seed(context);
            new MedicalScalesSeeder().Seed(context);
            context.SaveChanges();
        }
        catch (Exception exception)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "{Message}", exception.Message);
        }

        return host;
    }
}
