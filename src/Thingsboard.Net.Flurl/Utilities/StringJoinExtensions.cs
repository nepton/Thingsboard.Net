using System;
using System.Linq;

namespace Thingsboard.Net.Flurl.Utilities;

public static class StringJoinExtensions
{
    public static string? JoinWith<T>(this T[]? source)
    {
        return JoinWith(source, ",", t => t?.ToString());
    }

    public static string? JoinWith<T>(this T[]? source, string separator)
    {
        return JoinWith(source, separator, t => t?.ToString());
    }

    public static string? JoinWith<T>(this T[]? source, Func<T, string> selector)
    {
        return JoinWith<T>(source, ",", selector);
    }

    public static string? JoinWith<T>(this T[]? source, string separator, Func<T, string?> selector)
    {
        return source == null ? null : string.Join(separator, source.Select(selector));
    }
}
