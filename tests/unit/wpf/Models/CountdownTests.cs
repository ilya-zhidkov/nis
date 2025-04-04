using Nis.WpfApp.Models;

namespace Nis.WpfApp.UnitTests.Models;

public class CountdownTests : BaseUnitTest
{
    private readonly Countdown _countdown = new(TimeSpan.FromSeconds(5));

    [Fact]
    public void it_should_throw_if_countdown_has_not_started() => Assert.Equal("Countdown has not started yet.", Assert.Throws<InvalidOperationException>(() => _countdown.Stop()).Message);

    [Fact]
    public void it_should_throw_if_countdown_is_already_running()
    {
        _countdown.Start();

        Assert.Equal("Countdown is already running.", Assert.Throws<InvalidOperationException>(() => _countdown.Start()).Message);
    }

    [Fact]
    public void it_should_start_countdown()
    {
        _countdown.Start();

        Assert.True(_countdown.IsRunning);
    }

    [Fact]
    public void it_should_stop_countdown()
    {
        _countdown.Start();
        _countdown.Stop();

        Assert.False(_countdown.IsRunning);
    }
}
