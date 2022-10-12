using System;

namespace Thingsboard.Net.Flurl.Utilities.Implements;

public static class StringExtensions
{
    public static Guid? ToGuid(this string value)
    {
        if (Guid.TryParse(value, out var result))
        {
            return result;
        }

        return null;
    }

    public static bool? ToBoolean(this string value)
    {
        if (bool.TryParse(value, out var result))
        {
            return result;
        }

        return null;
    }
}
