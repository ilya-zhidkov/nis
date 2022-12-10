using Xunit;
using FluentAssertions;
using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.UnitTests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void it_should_return_null_when_merging_nulls() => ((Dictionary<int, string>)null).Merge(null).Should().BeNull();

    [Fact]
    public void it_should_merge_empty_dictionaries() => new Dictionary<int, string>().Merge(new Dictionary<int, string>()).Should().BeEmpty();

    [Fact]
    public void it_should_merge_distinct_dictionaries() => new Dictionary<int, string> { { 1, "a" } }
        .Merge(new Dictionary<int, string> { { 2, "b" } })
        .Should().BeEquivalentTo(new Dictionary<int, string> { { 1, "a" }, { 2, "b" } });

    [Fact]
    public void it_should_not_merge_duplicates() => new Dictionary<int, string> { { 1, "a" } }
        .Merge(new Dictionary<int, string> { { 1, "a" } })
        .Should().BeEquivalentTo(new Dictionary<int, string> { { 1, "a" } });

    [Fact]
    public void it_should_merge_first_if_second_is_null() => new Dictionary<int, string> { { 1, "a" } }
        .Merge(null)
        .Should().BeEquivalentTo(new Dictionary<int, string> { { 1, "a" } });

    [Fact]
    public void it_should_merge_second_if_first_is_null() => ((Dictionary<int, string>)null)
        .Merge(new Dictionary<int, string> { { 2, "b" } })
        .Should().BeEquivalentTo(new Dictionary<int, string> { { 2, "b" } });
}
