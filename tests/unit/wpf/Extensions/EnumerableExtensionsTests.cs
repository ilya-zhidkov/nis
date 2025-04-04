using Nis.WpfApp.Extensions;

namespace Nis.WpfApp.UnitTests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void it_should_return_null_when_merging_nulls() => Assert.Null(((Dictionary<int, string>)null!).Merge(null));

    [Fact]
    public void it_should_merge_empty_dictionaries() => Assert.Empty(new Dictionary<int, string>().Merge(new Dictionary<int, string>()));

    [Fact]
    public void it_should_merge_distinct_dictionaries() => Assert.Equivalent(
        expected: new Dictionary<int, string> { { 1, "a" } }.Merge(new Dictionary<int, string> { { 2, "b" } }),
        actual: new Dictionary<int, string> { { 1, "a" }, { 2, "b" } }
    );

    [Fact]
    public void it_should_not_merge_duplicates() => Assert.Equivalent(
        expected: new Dictionary<int, string> { { 1, "a" } },
        actual: new Dictionary<int, string> { { 1, "a" } }.Merge(new Dictionary<int, string> { { 1, "a" } })
    );

    [Fact]
    public void it_should_merge_first_if_second_is_null() => Assert.Equivalent(
        expected: new Dictionary<int, string> { { 1, "a" } },
        actual: new Dictionary<int, string> { { 1, "a" } }.Merge(null)
    );

    [Fact]
    public void it_should_merge_second_if_first_is_null() => Assert.Equivalent(
        expected: new Dictionary<int, string> { { 2, "b" } },
        actual: ((Dictionary<int, string>)null!).Merge(new Dictionary<int, string> { { 2, "b" } })
    );
}
