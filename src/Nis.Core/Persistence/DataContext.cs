using Nis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Nis.Core.Persistence.EntityConfigurations;

namespace Nis.Core.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            // ...
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PatientConfiguration());
        }
    }
}
