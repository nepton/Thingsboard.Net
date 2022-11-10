using System;

namespace Thingsboard.Net;

/// <summary>
/// The object extensions
/// </summary>
public static class TbObjectExtensions
{
    /// <summary>
    /// Converts the object to a specified type value.
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? To<T>(this object? source)
    {
        return source.To(default(T));
    }

    /// <summary>
    /// Converts the object to a specified type value.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="defaultValue">The default value</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? To<T>(this object? source, T? defaultValue)
    {
        if (source == null)
            return defaultValue;

        if (source is T expected)
            return expected;

        try
        {
            // If type is Nullable<T>, use underlyingType
            var type = typeof(T);
            if (Nullable.GetUnderlyingType(type) is { } underlyingType)
                type = underlyingType;

            // If T is of type Enum, use the enum.parse () conversion
            if (type.IsEnum)
            {
                var enumResult = source is string str ? (T) Enum.Parse(type, str, true) : (T) Enum.ToObject(type, source);
                return Enum.IsDefined(type, enumResult) ? enumResult : defaultValue;
            }

            var result = (T) Convert.ChangeType(source.ToString(), type);
            return result;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
}
