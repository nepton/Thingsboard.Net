using Newtonsoft.Json;
using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

/// <summary>
/// This class will test TbEntityField record
/// This record is used to identify a specific entity in Thingsboard
/// It can by created by ctor like this: var field = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
/// 
/// We test follow items:
/// 1. TbEntityField equals
/// 2. TbEntityField ==
/// 3. TbEntityField.GetHasCode() so we can use it in hash tables
/// 4. Read and Write JSON.
/// 5. Test edge cases like null, empty, etc.
/// </summary>
public class TbEntityFieldTester
{
    /// <summary>
    /// Test TbEntityField equals
    /// </summary>
    [Fact]
    public void TestEquals()
    {
        var field1 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        var field2 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        Assert.True(field1.Equals(field2));
    }

    /// <summary>
    /// Test TbEntityField ==
    /// </summary>
    [Fact]
    public void TestEqualOperator()
    {
        var field1 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        var field2 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        Assert.True(field1 == field2);
    }

    /// <summary>
    /// Test TbEntityField GetHashCode()
    /// </summary>
    [Fact]
    public void TestGetHashCode()
    {
        var field1 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        var field2 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        Assert.Equal(field1.GetHashCode(), field2.GetHashCode());
    }

    /// <summary>
    /// Test TbEntityField Read and Write JSON
    /// </summary>
    [Fact]
    public void TestJson()
    {
        var field1 = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        var json   = JsonConvert.SerializeObject(field1);
        var field2 = JsonConvert.DeserializeObject<TbEntityField>(json);
        Assert.True(field1.Equals(field2));
    }

    /// <summary>
    /// Test TbEntityField Null
    /// </summary>
    [Fact]
    public void TestNull()
    {
        TbEntityField? field = null;
        Assert.Null(field);
    }

    /// <summary>
    /// Test TbEntityField Empty
    /// </summary>
    [Fact]
    public void TestEmpty()
    {
        var field = new TbEntityField(null!, TbEntityFieldType.ATTRIBUTE);
        Assert.Null(field.Key);
        Assert.Equal(TbEntityFieldType.ATTRIBUTE, field.Type);
    }

    /// <summary>
    /// Test TbEntityField Empty
    /// </summary>
    [Fact]
    public void TestEmpty2()
    {
        var field = new TbEntityField("active", TbEntityFieldType.SERVER_ATTRIBUTE);
        Assert.NotEmpty(field.Key);
        Assert.Equal(TbEntityFieldType.SERVER_ATTRIBUTE, field.Type);
    }
}
