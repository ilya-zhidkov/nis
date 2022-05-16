using Nis.Core.Models;

namespace Nis.Api.Models;

public class Exam
{
    public string Diet { get; set; }
    public bool Passed { get; set; }
    public string Diagnosis { get; set; }
    public string Anamnesis { get; set; }
    public string Department { get; set; }
    public Student Student { get; set; }
}
