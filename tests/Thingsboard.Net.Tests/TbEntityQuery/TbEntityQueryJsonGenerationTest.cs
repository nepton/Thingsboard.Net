using Newtonsoft.Json;
using Quibble.Xunit;
using Thingsboard.Net.TbEntityQuery;

namespace Thingsboard.Net.Tests.TbEntityQuery;

public class TbEntityQueryJsonGenerationTest
{
    [Fact]
    public void TestApiUsageFilterWhenCustomerIdIsNull()
    {
        var request = new TbApiUsageFilter();
        var result = request.ToQuery();
        Assert.NotNull(result);

        var actual = JsonConvert.SerializeObject(result);
        JsonAssert.Equal("""{"type":"apiUsageState","customerId":null}""", actual);
    }

    [Fact]
    public void TestApiUsageFilterWhenCustomerIdIsNotNull()
    {
        var request = new TbApiUsageFilter
        {
            CustomerId = Guid.Empty
        };
        var result = request.ToQuery();
        Assert.NotNull(result);

        var actual = JsonConvert.SerializeObject(result);

        JsonAssert.Equal(
            """{"type":"apiUsageState","customerId":{"id":"00000000-0000-0000-0000-000000000000","entityType":"CUSTOMER"}}""",
            actual);
    }
}