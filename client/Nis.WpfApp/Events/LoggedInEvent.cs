using Nis.WpfApp.Models;

namespace Nis.WpfApp.Events;

public class LoggedInEvent
{
    public Student Student { get; }

    public LoggedInEvent(Student student) => Student = student;
}

