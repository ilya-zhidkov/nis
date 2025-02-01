using Caliburn.Micro;

namespace Nis.WpfApp.Models;

public sealed class MedicalScaleActivity : PropertyChangedBase
{
    private byte _score;
    private bool _isChecked;
    private string _name = null!;
    private readonly IEventAggregator _aggregator = IoC.Get<IEventAggregator>();

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
            _aggregator.PublishOnUIThreadAsync(this);
        }
    }

    public void Deconstruct(out string name, out byte score, out bool isChecked)
    {
        name = Name;
        score = Score;
        isChecked = IsChecked;
    }
}
