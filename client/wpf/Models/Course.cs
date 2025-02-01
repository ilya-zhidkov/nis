namespace Nis.WpfApp.Models;

[UsedImplicitly]
public sealed class Course
{
    public IEnumerable<Assignment> Assignments { get; init; } = (List<Assignment>) [];
}
