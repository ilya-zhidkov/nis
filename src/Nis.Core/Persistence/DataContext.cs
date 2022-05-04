﻿using Nis.Core.Models;
using Nis.Core.Extensions;
using Nis.Core.Models.Diagnosis;
using Microsoft.EntityFrameworkCore;
using Nis.Core.Persistence.EntityConfigurations;

namespace Nis.Core.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DataContext() : base() { /*..*/ }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { /* ... */ }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseSqlite(DatabaseExtensions.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PatientConfiguration());
        }
    }
}
