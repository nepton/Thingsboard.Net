using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDeviceProfileInfosTester
{
    [Fact]
    public async Task TestGetDeviceProfileInfos()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var deviceProfiles = await client.GetDeviceProfileInfosAsync(20, 0);

        // assert
        Assert.NotNull(deviceProfiles);
        Assert.NotEmpty(deviceProfiles.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var deviceProfiles = await client.GetDeviceProfileInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(deviceProfiles);
        Assert.Empty(deviceProfiles.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileInfosAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
