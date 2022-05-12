using Nis.Core.Models;

namespace Nis.Core.Persistence.Seeders;

public class DietSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {
        if (context.Diets.Any())
            return;

        context.Diets.AddRange(
            new Diet { Id = 1, Name = "Tekutá" },
            new Diet { Id = 4, Name = "Šetřicí" },
            new Diet { Id = 5, Name = "Racionální" },
            new Diet { Id = 6, Name = "S omezením tuků" },
            new Diet { Id = 7, Name = "Bezezbytková" },
            new Diet { Id = 8, Name = "Nízkobílkovinná" },
            new Diet { Id = 9, Name = "Nízkocholesterolová" },
            new Diet { Id = 10, Name = "Redukční" },
            new Diet { Id = 10, Name = "Diabetická" },
            new Diet { Id = 11, Name = "Neslaná šetřící" },
            new Diet { Id = 12, Name = "Výživná" },
            new Diet { Id = 13, Name = "Strava batolat" },
            new Diet { Id = 14, Name = "Strava větších dětí" },
            new Diet { Id = 15, Name = "Výběrová" }
        );
    }
}
