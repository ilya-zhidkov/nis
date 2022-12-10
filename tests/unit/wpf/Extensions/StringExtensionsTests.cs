using Xunit;
using FluentAssertions;
using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.UnitTests.Extensions;

public class StringExtensionsTests
{
    [Theory, InlineData(null), InlineData(""), InlineData(" ")]
    public void it_should_return_empty_string_when_invalid_input_is_passed(string input) => input.Capitalize().Should().BeEmpty();

    [Theory, InlineData("a", "A"), InlineData("lorem ipsum", "Lorem ipsum"), InlineData("LOREM IPSUM", "Lorem ipsum")]
    public void it_should_return_capitalized_string(string input, string expected) => input.Capitalize().Should().Be(expected);
}
