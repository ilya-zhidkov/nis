using Nis.Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Nis.Core.Extensions;

public static class DatabaseExtensions
{
    public static string ConnectionString = $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nis.db")}";

    public static DbContextOptions<DataContext> ConnectToDatabase(string connectionString) => new DbContextOptionsBuilder<DataContext>()
        .UseSqlite(connectionString)
        .Options;
}
