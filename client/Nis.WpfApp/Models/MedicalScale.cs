using Caliburn.Micro;

namespace Nis.WpfApp.Models;

public class MedicalScale
{
    public string Name { get; set; }
    public ScaleType Type { get; set; }
    public BindableCollection<MedicalScaleActivity> Activities { get; set; }

    public MedicalScale() => Activities = new BindableCollection<MedicalScaleActivity>();

    public enum ScaleType
    {
        Activity = 1,
        Decubitus = 2,
        Malnutrition = 3,
        RiskOfFall = 4
    }
}
