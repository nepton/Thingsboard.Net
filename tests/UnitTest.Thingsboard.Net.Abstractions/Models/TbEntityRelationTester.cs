using Newtonsoft.Json;
using Thingsboard.Net;
using Thingsboard.Net.TbEntityRelationModels;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

public class TbEntityRelationTester
{
    [Fact]
    public void TryParseJson()
    {
        var json = """
        [
          {
            "from": {
              "entityType": "ASSET",
              "id": "51091790-bcbb-11ed-8a71-975d50f4fbf1"
            },
            "to": {
              "entityType": "ASSET",
              "id": "60255900-bcbb-11ed-8a71-975d50f4fbf1"
            },
            "type": "Contains",
            "typeGroup": "COMMON",
            "additionalInfo": null
          }
        ]
        """;

        var relations = JsonConvert.DeserializeObject<TbEntityRelation[]>(json);
        Assert.NotNull(relations);
        Assert.Single(relations);
        Assert.Equal(TbEntityType.ASSET,                                 relations![0].From.EntityType);
        Assert.Equal(TbEntityType.ASSET,                                 relations[0].To.EntityType);
        Assert.Equal(Guid.Parse("51091790-bcbb-11ed-8a71-975d50f4fbf1"), relations[0].From.Id);
        Assert.Equal(Guid.Parse("60255900-bcbb-11ed-8a71-975d50f4fbf1"), relations[0].To.Id);
        Assert.Equal("Contains",                                         relations[0].Type);
    }
}
