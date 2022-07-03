using Xunit;
using FluentAssertions;
using Nis.WpfApp.Models;

namespace Nis.WpfApp.UnitTests.Models;

public class CountdownTests : BaseUnitTest
{
    private readonly Countdown _countdown;

    public CountdownTests() => _countdown = new Countdown(TimeSpan.FromSeconds(5));

    [Fact]
    public void it_should_throw_if_countdown_has_not_started() => _countdown.Invoking(countdown => countdown.Stop())
        .Should().Throw<InvalidOperationException>()
        .WithMessage("Countdown has not started yet.");

    [Fact]
    public void it_should_throw_if_countdown_is_already_running()
    {
        _countdown.Start();

        _countdown.Invoking(countdown => countdown.Start())
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Countdown is already running.");
    }

    [Fact]
    public void it_should_start_countdown()
    {
        _countdown.Start();
        _countdown.IsRunning.Should().BeTrue();
    }

    [Fact]
    public void it_should_stop_countdown()
    {
        _countdown.Start();
        _countdown.Stop();
        _countdown.IsRunning.Should().BeFalse();
    }
}
