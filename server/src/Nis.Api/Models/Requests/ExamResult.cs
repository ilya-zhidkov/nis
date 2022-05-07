using Nis.Core.Models;

namespace Nis.Api.Models.Requests;

public class ExamResult
{
    public string Diet { get; set; }
    public bool Passed { get; set; }
    public string Diagnose { get; set; }
    public string Anamnesis { get; set; }
    public string Department { get; set; }
    public Student Student { get; set; }
}
