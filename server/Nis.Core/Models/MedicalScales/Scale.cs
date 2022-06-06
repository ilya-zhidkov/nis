using Nis.Core.Models.Enums;

namespace Nis.Core.Models.MedicalScales;

public class Scale : BaseEntity
{
    public string Name { get; set; }
    public ScaleType ScaleType { get; set; }
    public IEnumerable<ScaleActivity> Activities { get; set; } = new List<ScaleActivity>();
}

