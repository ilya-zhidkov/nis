using Nis.Core.Models;
using Nis.Core.Factories;

namespace Nis.Core.Persistence.Seeders;

public class PatientSeeder : BaseSeeder
{
    private readonly BaseFactory<Patient> _factory;

    public PatientSeeder() => _factory = new PatientFactory();

    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Patients.Any())
            return;

        context.Patients.AddRange(_factory.Create(count: 10));
    }
}
