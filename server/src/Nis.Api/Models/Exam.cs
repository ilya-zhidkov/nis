using Nis.Core.Models;
using Nis.Core.Models.MedicalScales;

namespace Nis.Api.Models;

public class Exam
{
    public string Diet { get; set; }
    public bool Passed { get; set; }
    public string Diagnosis { get; set; }
    public string Anamnesis { get; set; }
    public string Department { get; set; }
    public Student Student { get; set; }
    public IEnumerable<MedicalScale> MedicalScales { get; set; } = new List<MedicalScale>();
}
