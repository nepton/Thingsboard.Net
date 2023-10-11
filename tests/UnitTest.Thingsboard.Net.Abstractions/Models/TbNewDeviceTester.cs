using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Abstractions.Models;

public class TbNewDeviceTester
{
    [Fact]
    public void TestGetIsGateway()
    {
        var device = new TbNewDevice();
        Assert.False(device.IsGateway);

        device.AdditionalInfo["gateway"] = true;
        Assert.True(device.IsGateway);

        device.AdditionalInfo["gateway"] = false;
        Assert.False(device.IsGateway);

        device.AdditionalInfo.Remove("gateway");
        Assert.False(device.IsGateway);
    }

    [Fact]
    public void TestSetIsGateway()
    {
        var device = new TbNewDevice();
        Assert.False(device.IsGateway);

        device.IsGateway = true;
        Assert.True(device.IsGateway);
        Assert.True(device.AdditionalInfo.ContainsKey("gateway"));
        Assert.True((bool) device.AdditionalInfo["gateway"]!);

        device.IsGateway = false;
        Assert.False(device.IsGateway);
        Assert.True(device.AdditionalInfo.ContainsKey("gateway"));
        Assert.False((bool) device.AdditionalInfo["gateway"]!);

        device.IsGateway = true;
        Assert.True(device.IsGateway);
        Assert.True(device.AdditionalInfo.ContainsKey("gateway"));
        Assert.True((bool) device.AdditionalInfo["gateway"]!);
    }
}
