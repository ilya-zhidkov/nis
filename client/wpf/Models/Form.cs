namespace Nis.WpfApp.Models;

public sealed class Form
{
    public bool Passed { get; set; }
    public required string? Anamnesis { get; set; }
    public required string Diagnosis { get; set; }
    public required string Department { get; set; }
    public required string Diet { get; set; }
    public required Student Student { get; set; }
    public IEnumerable<MedicalScale> Scales { get; set; } = new List<MedicalScale>();
}
