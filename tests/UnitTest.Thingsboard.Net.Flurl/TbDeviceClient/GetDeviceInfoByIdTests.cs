using Newtonsoft.Json;
using Quibble.Xunit;
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
        var deviceInfo = await client.GetDeviceInfoByIdAsync(TbTestData.GetTestDeviceId());

        Assert.NotNull(deviceInfo);
        Assert.Equal(TbTestData.GetTestDeviceId(), deviceInfo.Id.Id);
        Assert.Equal(TbTestData.TestDeviceName,    deviceInfo.Name);
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
