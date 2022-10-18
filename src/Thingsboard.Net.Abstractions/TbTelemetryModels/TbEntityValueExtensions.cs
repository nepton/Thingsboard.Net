using System;
using System.Collections.Generic;
using System.Linq;

namespace Thingsboard.Net;

public static class TbEntityValueExtensions
{
    public static TbEntityValue? GetEntityValue(this IEnumerable<TbEntityValue>? source, TbEntityField key)
    {
        if (source == null)
            return null;

        return source.FirstOrDefault(x => x.Key == key.Key);
    }

    /// <summary>
    /// Gets the update date of the entity value
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static DateTime? GetTime(this IEnumerable<TbEntityValue>? source, TbEntityField key)
    {
        return source.GetEntityValue(key)?.LastUpdateTs;
    }

    /// <summary>
    /// Looks for the specified value in the collection and, if found, returns the default value for the specified type TValue
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="key"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? GetValue<TValue>(this IEnumerable<TbEntityValue>? entities, TbEntityField key)
    {
        return GetValue<TValue>(entities, key, default);
    }

    /// <summary>
    /// Gets the value of the specified Key in the collection. If it does not exist, returns the default value
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? GetValue<TValue>(this IEnumerable<TbEntityValue>? entities, TbEntityField key, TValue? defaultValue)
    {
        if (entities == null)
            return defaultValue;

        var entity = entities.FirstOrDefault(x => x.Key == key.Key);
        if (entity == null)
            return defaultValue;

        var value = entity.Value;

        if (value is null)
            return defaultValue;

        if (value is TValue expected)
            return expected;

        try
        {
            // 如果 type 是 Nullable<T> 的话, 使用 underlyingType
            var type = typeof(TValue);
            if (Nullable.GetUnderlyingType(type) is { } underlyingType)
                type = underlyingType;

            // 如果T是Enum类型, 使用Enum.Parse()转换
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
