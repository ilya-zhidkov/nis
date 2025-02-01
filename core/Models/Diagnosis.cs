namespace Nis.Core.Models;

public class Diagnosis : BaseEntity
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
