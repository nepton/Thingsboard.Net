using Newtonsoft.Json;
using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

public class TbKeyFilterTests
{
    [Fact]
    public void TestParseBooleanKeyFilter()
    {
        // arrange
        var json = """
        {
            "operation": "EQUAL",
            "value": {
              "defaultValue": true
            }
        }
        """;

        // act
        var filter = JsonConvert.DeserializeObject<TbKeyFilterBooleanValuePredicate>(json);

        // assert
        Assert.NotNull(filter);
        Assert.Equal(TbKeyFilterBooleanOperation.EQUAL, filter.Operation);
        Assert.NotNull(filter.Value);
        Assert.Equal(true, filter.Value.DefaultValue);
    }
}
