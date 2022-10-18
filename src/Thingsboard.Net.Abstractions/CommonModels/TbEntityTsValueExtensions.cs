using System;
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
    /// Gets the update date of the entity value
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static DateTime? GetTime(this IReadOnlyDictionary<TbEntityField, TbEntityTsValue> source, TbEntityField key)
    {
        return source.GetEntityTsValue(key)?.Ts;
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

        var value = entity.Value;

        if (value is null)
            return defaultValue;

        if (value is TValue expected)
            return expected;

        try
        {
            // If type is Nullable<T>, use underlyingType
            var type = typeof(TValue);
            if (Nullable.GetUnderlyingType(type) is { } underlyingType)
                type = underlyingType;

            // If T is of type Enum, use the enum.parse () conversion
            if (type.IsEnum)
            {
                return (TValue) Enum.Parse(typeof(TValue), value.ToString() ?? "");
            }

            var result = (TValue?) Convert.ChangeType(value.ToString(), type);
            return result;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
}
