using Newtonsoft.Json;
using Quibble.Xunit;
using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetDeviceInfoByIdTests
{
    [Fact]
    public async Task TestGetDeviceInfoById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId   = Guid.Parse("ab5371c0-47a2-11ed-8248-233ce934eba0");
        var deviceInfo = await client.GetDeviceInfoByIdAsync(deviceId);

        Assert.NotNull(deviceInfo);

        var json = JsonConvert.SerializeObject(deviceInfo);
        var expected =
            """{"Id":{"Id":"ab5371c0-47a2-11ed-8248-233ce934eba0","EntityType":6},"TenantId":{"Id":"aaf39e80-47a2-11ed-8248-233ce934eba0","EntityType":15},"CustomerId":{"Id":"ab23af30-47a2-11ed-8248-233ce934eba0","EntityType":4},"Name":"Test Device A1","Type":"default","Label":null,"DeviceProfileId":{"Id":"aaf7e440-47a2-11ed-8248-233ce934eba0","EntityType":7},"FirmwareId":null,"SoftwareId":null,"CustomerTitle":"Customer A","CustomerIsPublic":false,"DeviceProfileName":"default"}""";
        JsonAssert.EqualOverrideDefault(expected, json, new JsonDiffConfig(true));
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId   = Guid.Empty;
        var deviceInfo = await client.GetDeviceInfoByIdAsync(deviceId);

        Assert.Null(deviceInfo);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceInfoByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceInfoByIdAsync(Guid.NewGuid());
            });
    }
}
