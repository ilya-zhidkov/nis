using Nis.Core.Models;
using Nis.Core.Extensions;
using Nis.Core.Models.MedicalScales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nis.Core.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Diet> Diets { get; set; }
    public DbSet<Scale> Scales { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<ScaleActivity> ScaleActivities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder) => builder.UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    internal class DesignTimeContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args) => new(DatabaseExtensions.ConnectToDatabase(DatabaseExtensions.ConnectionString));
    }
}
