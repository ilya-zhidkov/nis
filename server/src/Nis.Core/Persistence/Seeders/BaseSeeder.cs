namespace Nis.Core.Persistence.Seeders;

public abstract class BaseSeeder
{
    public abstract void Seed(DataContext context, IServiceProvider services = null);
}
