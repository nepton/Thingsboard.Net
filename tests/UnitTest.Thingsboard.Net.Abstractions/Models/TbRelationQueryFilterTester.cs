using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

using Newtonsoft.Json;
using Xunit;

public class TbRelationQueryFilterTests
{
    [Fact]
    public void CanDeserializeJson()
    {
        // Arrange
        var json = """
        {
            "type": "relationsQuery",
            "rootEntity": {
              "entityType": "DEVICE",
              "id": "848aba50-a1ff-11ed-9135-d3a6ac6c1c62"
            },
            "direction": "TO",
            "maxLevel": 100,
            "fetchLastLevelOnly": false,
            "filters": [
              {
                "relationType": "Contains",
                "entityTypes": [
                  "DEVICE",
                  "ASSET"
                ]
              }
            ]
          }
        """;

        // Act
        var result = JsonConvert.DeserializeObject<TbRelationsQueryFilter>(json);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("relationsQuery",                                   result.Type);
        Assert.Equal(TbEntityType.DEVICE,                                result.RootEntity.EntityType);
        Assert.Equal(Guid.Parse("848aba50-a1ff-11ed-9135-d3a6ac6c1c62"), result.RootEntity.Id);
        Assert.Equal(TbRelationDirection.TO,                             result.Direction);
        Assert.Equal(100,                                                result.MaxLevel);
        Assert.False(result.FetchLastLevelOnly);
        Assert.Single(result.Filters);
        Assert.Equal("Contains",                                      result.Filters[0].RelationType);
        Assert.Equal(new[] {TbEntityType.DEVICE, TbEntityType.ASSET}, result.Filters[0].EntityTypes);
    }
}
