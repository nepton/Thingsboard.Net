using Newtonsoft.Json;
using Quibble.Xunit;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetDeviceByIdTests
{
    [Fact]
    public async Task TestGetDeviceById()
    {
        // arrange
        var api = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId = Guid.Parse("ab5371c0-47a2-11ed-8248-233ce934eba0");
        var device   = await api.GetDeviceByIdAsync(deviceId);

        Assert.NotNull(device);

        var json = JsonConvert.SerializeObject(device);
        var expected =
            """{"Id":{"Id":"ab5371c0-47a2-11ed-8248-233ce934eba0","EntityType":6},"TenantId":{"Id":"aaf39e80-47a2-11ed-8248-233ce934eba0","EntityType":15},"CustomerId":{"Id":"ab23af30-47a2-11ed-8248-233ce934eba0","EntityType":4},"Name":"Test Device A1","Type":"default","Label":null,"DeviceProfileId":{"Id":"aaf7e440-47a2-11ed-8248-233ce934eba0","EntityType":7},"FirmwareId":null,"SoftwareId":null}""";
        JsonAssert.EqualOverrideDefault(expected, json, new JsonDiffConfig(true));
    }

    [Fact]
    public async Task WhenDeviceNotFound()
    {
        // arrange
        var api = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId = Guid.Empty;
        var device   = await api.GetDeviceByIdAsync(deviceId);

        Assert.Null(device);
    }
}
