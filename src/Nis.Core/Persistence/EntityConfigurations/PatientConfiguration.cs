using Nis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nis.Core.Persistence.EntityConfigurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
