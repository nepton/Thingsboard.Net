using System.Collections.Generic;

namespace Thingsboard.Net;

public static class TbEntityTsValueExtensions
{
    public static TbEntityTsValue[]? GetEntityTsValues(this IReadOnlyDictionary<TbEntityField, TbEntityTsValue[]>? source, TbEntityField key)
    {
        if (source == null)
            return null;

        return source.TryGetValue(key, out var value) ? value : null;
    }

    public static TbEntityTsValue? GetEntityTsValue(this IReadOnlyDictionary<TbEntityField, TbEntityTsValue>? source, TbEntityField key)
    {
        if (source == null)
            return null;

        return source.TryGetValue(key, out var value) ? value : null;
    }

    /// <summary>
    /// Looks for the specified value in the collection and, if found, returns the default value for the specified type TValue
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? GetValue<TValue>(this IReadOnlyDictionary<TbEntityField, TbEntityTsValue>? source, TbEntityField key)
    {
        return GetValue<TValue>(source, key, default);
    }

    /// <summary>
    /// Gets the value of the specified Key in the collection. If it does not exist, returns the default value
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? GetValue<TValue>(this IReadOnlyDictionary<TbEntityField, TbEntityTsValue>? source, TbEntityField key, TValue? defaultValue)
    {
        if (source == null)
            return defaultValue;

        var entity = GetEntityTsValue(source, key);
        if (entity == null)
            return defaultValue;

        return entity.Value.To(defaultValue);
    }
}
