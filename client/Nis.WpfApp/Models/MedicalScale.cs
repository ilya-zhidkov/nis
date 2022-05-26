using Caliburn.Micro;

namespace Nis.WpfApp.Models;

public class MedicalScale
{
    public string Name { get; set; }
    public ScaleType ScaleType { get; set; }
    public BindableCollection<MedicalScaleActivity> Activities { get; set; }

    public MedicalScale() => Activities = new BindableCollection<MedicalScaleActivity>();
}
