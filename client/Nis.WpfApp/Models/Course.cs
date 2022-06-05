namespace Nis.WpfApp.Models;

public class Course
{
    public IEnumerable<Assignment> Assignments { get; set; }

    public Course() => Assignments = new List<Assignment>();
}
