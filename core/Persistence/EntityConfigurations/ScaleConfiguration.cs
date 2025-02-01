using Nis.Core.Models.MedicalScales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nis.Core.Persistence.EntityConfigurations;

public class ScaleConfiguration : IEntityTypeConfiguration<Scale>
{
    public void Configure(EntityTypeBuilder<Scale> builder) => builder.ToTable("scales");
}

