using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.Models;

public class Assignment
{
    private string _intro;

    public string Intro
    {
        get => _intro.Sanitize();
        set => _intro = value;
    }
}
