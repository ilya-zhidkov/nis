namespace Nis.WpfApp.Extensions;

public static class EnumerableExtensions
{
    public static IDictionary<TKey, TValue> Merge<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        IDictionary<TKey, TValue> with
    ) => dictionary switch
    {
        null when with is not null => new Dictionary<TKey, TValue>().Union(with).ToDictionary(pair => pair.Key, pair => pair.Value),
        _ => with is null ? dictionary : dictionary.Union(with).ToDictionary(pair => pair.Key, pair => pair.Value)
    };
}
