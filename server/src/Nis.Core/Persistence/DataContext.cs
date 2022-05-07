﻿using Nis.Core.Models;
using Nis.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Nis.Core.Persistence;

public class DataContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Diagnose> Diagnoses { get; set; }
    public DbSet<Department> Departments { get; set; }

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
