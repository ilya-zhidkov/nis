using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.Models;

[UsedImplicitly]
public sealed class Assignment
{
    private string? _intro;

    public required string? Intro
    {
        get => _intro?.Sanitize();
        set => _intro = value;
    }
}
