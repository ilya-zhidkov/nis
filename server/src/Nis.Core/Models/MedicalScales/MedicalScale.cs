using Nis.Core.Models.Enums;

namespace Nis.Core.Models.MedicalScales;

public class MedicalScale : BaseEntity
{
    public string Name { get; set; }
    public MedicalScaleCategory ScaleCategory { get; set; }
    public IEnumerable<MedicalScaleActivity> Activities { get; set; } = new List<MedicalScaleActivity>();
}