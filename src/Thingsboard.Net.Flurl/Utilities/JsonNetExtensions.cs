using System;
using Newtonsoft.Json;

namespace Thingsboard.Net.Flurl.Utilities;

public static class JsonNetExtensions
{
    public static T? TryDeserializeObject<T>(this string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception)
        {
            return default;
        }
    }
}
