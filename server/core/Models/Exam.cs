namespace Nis.Core.Models;

public class Exam : BaseEntity
{
    public string Anamnesis { get; set; }
    public Diet Diet { get; set; }
    public int DietId { get; set; }
    public int DiagnosisId { get; set; }
    public Diagnosis Diagnosis { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
