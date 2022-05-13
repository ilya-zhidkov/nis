using Nis.Core.Models;

namespace Nis.Core.Persistence.Seeders;

public class DietSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Diets.Any())
            return;

        context.Diets.AddRange(
            new Diet { Name = "Tekutá" },
            new Diet { Name = "Šetřicí" },
            new Diet { Name = "Racionální" },
            new Diet { Name = "S omezením tuků" },
            new Diet { Name = "Bezezbytková" },
            new Diet { Name = "Nízkobílkovinná" },
            new Diet { Name = "Nízkocholesterolová" },
            new Diet { Name = "Redukční" },
            new Diet { Name = "Diabetická" },
            new Diet { Name = "Neslaná šetřící" },
            new Diet { Name = "Výživná" },
            new Diet { Name = "Strava batolat" },
            new Diet { Name = "Strava větších dětí" },
            new Diet { Name = "Výběrová" }
        );
    }
}