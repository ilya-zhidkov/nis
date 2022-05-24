using Caliburn.Micro;

namespace Nis.WpfApp.Models;

public class MedicalScaleActivity : PropertyChangedBase
{
    private byte _score;
    private string _name;
    private bool _isChecked;

    public byte Score
    {
        get => _score;
        set
        {
            _score = value;
            NotifyOfPropertyChange(() => Score);
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            NotifyOfPropertyChange(() => Name);
        }
    }

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            NotifyOfPropertyChange(() => IsChecked);
        }
    }
}
