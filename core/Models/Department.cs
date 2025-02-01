namespace Nis.Core.Models;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
}
