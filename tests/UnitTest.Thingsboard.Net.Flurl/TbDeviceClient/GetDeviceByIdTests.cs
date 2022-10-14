using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetDeviceByIdTests
{
    [Fact]
    public async Task TestGetDeviceById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDeviceByIdAsync(TbTestData.TestDeviceId);

        Assert.NotNull(actual);

        var json = JsonConvert.SerializeObject(actual);
        var expected =
            """{"Id":{"Id":"ab5371c0-47a2-11ed-8248-233ce934eba0","EntityType":6},"TenantId":{"Id":"aaf39e80-47a2-11ed-8248-233ce934eba0","EntityType":15},"CustomerId":{"Id":"ab23af30-47a2-11ed-8248-233ce934eba0","EntityType":4},"Name":"Test Device A1","Type":"default","Label":null,"DeviceProfileId":{"Id":"aaf7e440-47a2-11ed-8248-233ce934eba0","EntityType":7},"FirmwareId":null,"SoftwareId":null}""";
        JsonAssert.EqualOverrideDefault(expected, json, new JsonDiffConfig(true));
    }

    [Fact]
    public async Task TestGetDeviceByIdWhenNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual   = await client.GetDeviceByIdAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceByIdAsync(Guid.NewGuid());
            });
    }
}
