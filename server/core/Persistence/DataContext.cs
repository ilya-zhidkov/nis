using Nis.Core.Models;
using Nis.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Nis.Core.Models.MedicalScales;
using Microsoft.EntityFrameworkCore.Design;

namespace Nis.Core.Persistence;

public class DataContext : DbContext
{
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Diet> Diets { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Scale> Scales { get; set; }
    public DbSet<ScaleActivity> ScaleActivities { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { /* ... */ }

    protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args) => new(
            DatabaseExtensions.ConnectToDatabase(DatabaseExtensions.ConnectionString)
        );
    }
}
