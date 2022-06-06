using Nis.Core.Models;

namespace Nis.Core.Persistence.Seeders;

public class DepartmentSeeder : BaseSeeder
{
    public override void Seed(DataContext context, IServiceProvider services = null)
    {

        if (context.Departments.Any())
            return;

        context.Departments.AddRange(
            new Department { Id = 1, Name = "Infekční oddělení" },
            new Department { Id = 2, Name = "Oddělení hematologie" },
            new Department { Id = 3, Name = "Oddělení endokrinologie a diabetologie" },
            new Department { Id = 4, Name = "Oddělení lékařské genetiky" },
            new Department { Id = 5, Name = "Neurologické oddělení" },
            new Department { Id = 6, Name = "Kardiologie" },
            new Department { Id = 7, Name = "Plicní oddělení" },
            new Department { Id = 8, Name = "Gastroenterologie" },
            new Department { Id = 9, Name = "Ortopedie" },
            new Department { Id = 10, Name = "Onkologie" },
            new Department { Id = 11, Name = "Nefrologie a urologie" }
        );
    }
}
