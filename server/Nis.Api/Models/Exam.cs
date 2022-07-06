using Nis.Core.Models;
using Nis.Core.Models.MedicalScales;

namespace Nis.Api.Models;

public sealed class Exam
{
    public string Diet { get; set; }
    public bool Passed { get; set; }
    public string Diagnosis { get; set; }
    public string Anamnesis { get; set; }
    public string Department { get; set; }
    public Student Student { get; set; }
    public IEnumerable<Scale> Scales { get; set; } = new List<Scale>();
}
