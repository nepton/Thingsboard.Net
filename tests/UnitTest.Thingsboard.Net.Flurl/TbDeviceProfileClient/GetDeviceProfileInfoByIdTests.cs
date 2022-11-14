using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDeviceProfileInfoByIdTests
{
    [Fact]
    public async Task TestGetDeviceProfileInfoById()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateDeviceProfileClient();
        var expected = await client.GetDefaultDeviceProfileInfoAsync();
        Assert.NotNull(expected);

        // act
        var deviceProfileInfo = await client.GetDeviceProfileInfoByIdAsync(expected.Id.Id);

        Assert.NotNull(deviceProfileInfo);
        Assert.Equal(expected.Id,   deviceProfileInfo.Id);
        Assert.Equal(expected.Name, deviceProfileInfo.Name);
    }

    [Fact]
    public async Task TestWhenDeviceProfileNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var deviceProfileId   = Guid.Empty;
        var deviceProfileInfo = await client.GetDeviceProfileInfoByIdAsync(deviceProfileId);

        Assert.Null(deviceProfileInfo);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileInfoByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileInfoByIdAsync(Guid.NewGuid());
            });
    }
}
