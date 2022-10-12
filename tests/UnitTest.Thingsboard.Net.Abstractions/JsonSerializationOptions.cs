using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace UnitTest.Thingsboard.Net.Abstractions.Utility.Json;

public class TestJsonSettings
{
    public static JsonSerializerSettings GetJsonSerializerSettings()
    {
        // Setup for newtonsoft json
        return new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<JsonConverter>()
                {
                    new StringEnumConverter(),
                    new JavaScriptTicksDateTimeConverter() // tb uses javascript ticks for dates
                },
            };
    }

}
