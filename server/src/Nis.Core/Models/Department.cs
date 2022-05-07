namespace Nis.Core.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Diagnose> Diagnoses { get; set; } = new List<Diagnose>();
}
