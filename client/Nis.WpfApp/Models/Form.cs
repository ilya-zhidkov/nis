namespace Nis.WpfApp.Models;

public class Form
{
    public bool Passed { get; set; } = true;
    public string Anamnesis { get; set; }
    public string Diagnosis { get; set; }
    public string Department { get; set; }
    public string Diet { get; set; }
    public Student Student { get; set; }
}
