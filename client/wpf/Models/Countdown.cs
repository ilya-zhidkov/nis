using System.Windows.Threading;

namespace Nis.WpfApp.Models;

public sealed class Countdown(TimeSpan interval)
{
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };

    public event EventHandler? Tick;
    public bool IsRunning => _timer.IsEnabled;
    public TimeSpan Interval { get; private set; } = interval;

    public void Start()
    {
        if (IsRunning)
            throw new InvalidOperationException("Countdown is already running.");

        _timer.Tick += Tick ?? ((_, _) =>
        {
            if (Interval <= TimeSpan.Zero)
                Stop();

            Interval = Interval.Subtract(TimeSpan.FromSeconds(1));
        });

        _timer.Start();
    }

    public void Stop()
    {
        if (!IsRunning)
            throw new InvalidOperationException("Countdown has not started yet.");

        _timer.Stop();
        _timer.Tick -= Tick;
    }
}
