using Nis.Core.Models.MedicalScales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nis.Core.Persistence.EntityConfigurations;

public class ScaleActivityConfiguration : IEntityTypeConfiguration<ScaleActivity>
{
    public void Configure(EntityTypeBuilder<ScaleActivity> builder) => builder.ToTable("scale_activities");
}

