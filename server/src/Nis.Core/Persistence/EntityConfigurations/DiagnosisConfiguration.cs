using Nis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nis.Core.Persistence.EntityConfigurations;

public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder
            .Property(diagnosis => diagnosis.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .HasOne(diagnosis => diagnosis.Department)
            .WithMany(department => department.Diagnoses);
    }
}
