using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Quibble.Xunit;
using Thingsboard.Net.TbEntityQuery;

namespace Thingsboard.Net.Tests.TbEntityQuery;

public class TbEntityQueryJsonGenerationTest
{
    [Fact]
    public void TestApiUsageFilterWhenCustomerIdIsNull()
    {
        var request = new TbApiUsageFilter();
        var result  = request.ToQuery();
        Assert.NotNull(result);

        var actual = JsonConvert.SerializeObject(result, JsonSerializerSettings);
        JsonAssert.Equal("""{"type":"apiUsageState","customerId":null}""", actual);
    }

    [Fact]
    public void TestApiUsageFilterWhenCustomerIdIsNotNull()
    {
        var request = new TbApiUsageFilter(Guid.Empty);
        var result  = request.ToQuery();
        Assert.NotNull(result);

        var actual = JsonConvert.SerializeObject(result, JsonSerializerSettings);

        JsonAssert.Equal(
            """{"customerId":{"id":"00000000-0000-0000-0000-000000000000","entityType":"CUSTOMER"},"type":"apiUsageState"}""",
            actual);
    }

    private static JsonSerializerSettings JsonSerializerSettings
    {
        get
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }
    }
}
