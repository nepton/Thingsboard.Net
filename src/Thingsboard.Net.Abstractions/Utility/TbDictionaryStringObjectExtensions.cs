using System;
using System.Collections.Generic;
using System.Linq;

namespace Thingsboard.Net;

public static class TbDictionaryStringObjectExtensions
{

    /// <summary>
    /// Looks for the specified value in the collection and, if found, returns the default value for the specified type TValue
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="key"></param>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    public static TValue? GetValue<TValue>(this Dictionary<string, object?>? entities, TbEntityField key)
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
    public static TValue? GetValue<TValue>(this IEnumerable<TbEntityKeyValue>? entities, TbEntityField key, TValue? defaultValue)
    {
        if (entities == null)
            return defaultValue;

        var entity = entities.FirstOrDefault(x => x.Key == key.Key);
        if (entity == null)
            return defaultValue;

        return entity.Value.To(defaultValue);
    }
}
