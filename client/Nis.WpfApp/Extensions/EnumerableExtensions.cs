namespace Nis.WpfApp.Extensions;

public static class EnumerableExtensions
{
    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        IDictionary<TKey, TValue> with
    ) => dictionary.Union(with).ToDictionary(pair => pair.Key, pair => pair.Value);
}
