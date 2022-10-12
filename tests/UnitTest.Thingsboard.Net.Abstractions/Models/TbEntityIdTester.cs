using Newtonsoft.Json;
using Quibble.Xunit;
using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Abstractions.Utility.Json;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

/// <summary>
/// This class will test TbEntityId record
/// This record is used to identify a specific entity in Thingsboard
/// It can by created by ctor like this: var id = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
/// 
/// We test follow items:
/// 1. TbEntityId equals ( include EntityType and Id ) and not equals.
/// 2. TbEntityId == and !=
/// 3. TbEntityId.GetHasCode() so we can use it in hash tables
/// 4. Read and Write JSON.
/// 5. Test JSON string format. The JSON format mostly like this: {"id": "784f394c-42b6-435a-983c-b7beff2784f9", "entityType": "DEVICE"},
/// </summary>
public class TbEntityIdTester
{
    public TbEntityIdTester()
    {
        JsonConvert.DefaultSettings = TestJsonSettings.GetJsonSerializerSettings;
    }

    /// <summary>
    /// Test TbEntityId equals ( include EntityType and Id ) and not equals.
    /// </summary>
    [Fact]
    public void TestEquals()
    {
        var id1 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
        var id2 = new TbEntityId(TbEntityType.DEVICE, id1.Id);
        var id3 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());

        Assert.True(id1.Equals(id2));
        Assert.False(id1.Equals(id3));
    }

    /// <summary>
    /// Test TbEntityId == and !=
    /// </summary>
    [Fact]
    public void TestOperator()
    {
        var id1 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
        var id2 = new TbEntityId(TbEntityType.DEVICE, id1.Id);
        var id3 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());

        Assert.True(id1 == id2);
        Assert.False(id1 != id2);
        Assert.False(id1 == id3);
        Assert.True(id1 != id3);
    }

    /// <summary>
    /// Test TbEntityId.GetHasCode() so we can use it in hash tables
    /// </summary>
    [Fact]
    public void TestGetHasCode()
    {
        var id1 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
        var id2 = new TbEntityId(TbEntityType.DEVICE, id1.Id);
        var id3 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());

        Assert.Equal(id1.GetHashCode(), id2.GetHashCode());
        Assert.NotEqual(id1.GetHashCode(), id3.GetHashCode());
    }

    /// <summary>
    /// Test Read and Write JSON.
    /// </summary>
    [Fact]
    public void TestJsonSerialization()
    {
        var id1 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
        var id2 = new TbEntityId(TbEntityType.DEVICE, id1.Id);
        var id3 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());

        var json1 = JsonConvert.SerializeObject(id1);
        var json2 = JsonConvert.SerializeObject(id2);
        var json3 = JsonConvert.SerializeObject(id3);

        var backId1 = JsonConvert.DeserializeObject<TbEntityId>(json1);
        var backId2 = JsonConvert.DeserializeObject<TbEntityId>(json2);
        var backId3 = JsonConvert.DeserializeObject<TbEntityId>(json3);

        Assert.Equal(id1, backId1);
        Assert.Equal(id2, backId2);
        Assert.Equal(id3, backId3);
    }

    /// <summary>
    /// Test JSON string format. The JSON format mostly like this: {"id": "784f394c-42b6-435a-983c-b7beff2784f9", "entityType": "DEVICE"},
    /// </summary>
    [Fact]
    public void TestJsonString()
    {
        var id1 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());
        var id2 = new TbEntityId(TbEntityType.DEVICE, id1.Id);
        var id3 = new TbEntityId(TbEntityType.DEVICE, Guid.NewGuid());

        var json1 = JsonConvert.SerializeObject(id1);
        var json2 = JsonConvert.SerializeObject(id2);
        var json3 = JsonConvert.SerializeObject(id3);

        var expected1 = $$"""{"id": "{{id1.Id}}", "entityType": "{{id1.EntityType}}"}""";
        JsonAssert.Equal(expected1, json1);

        var expected2 = $$"""{"id": "{{id2.Id}}", "entityType": "{{id2.EntityType}}"}""";
        JsonAssert.Equal(expected2, json2);

        var expected3 = $$"""{"id": "{{id3.Id}}", "entityType": "{{id3.EntityType}}"}""";
        JsonAssert.Equal(expected3, json3);
    }
}
