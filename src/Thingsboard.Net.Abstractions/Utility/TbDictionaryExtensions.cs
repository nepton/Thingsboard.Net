using System.Collections.Generic;

namespace Thingsboard.Net;

public static class TbDictionaryExtensions
{
    /// <summary>
    /// Returns the value to which the specified key is mapped, or default if this map contains no mapping for the key.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key, TValue? defaultValue)
    {
        if (source.TryGetValue(key, out var obj))
        {
            defaultValue = obj;
            return defaultValue;
        }

        return defaultValue;
    }

    /// <summary>
    /// Returns the value to which the specified key is mapped, or default if this map contains no mapping for the key. 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source, TKey key)
    {
        if (source.TryGetValue(key, out var obj))
        {
            return obj;
        }

        return default;
    }
}
