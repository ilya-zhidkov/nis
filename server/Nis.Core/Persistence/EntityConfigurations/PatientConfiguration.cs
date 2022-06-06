using Nis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nis.Core.Persistence.EntityConfigurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(patient => patient.Id);

        builder
            .Property(patient => patient.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(patient => patient.LastName)
            .IsRequired()
            .HasMaxLength(50);
    }
}
