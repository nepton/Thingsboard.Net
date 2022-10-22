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
    public static T? To<T>(this object source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
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
                return (T) Enum.Parse(typeof(T), source.ToString() ?? "");
            }

            var result = (T) Convert.ChangeType(source.ToString(), type);
            return result;
        }
        catch (Exception)
        {
            return default;
        }
    }
}
