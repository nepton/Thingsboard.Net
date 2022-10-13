using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetTenantDevicesTester
{
    [Fact]
    public async Task TestGenerals()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDevicesAsync(20, 0);

        // assert
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Data);
    }

    [Fact]
    public async Task TestWhenNoDataFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDevicesAsync(20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual.Data);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDevicesAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDevicesAsync(20, 0, textSearch: Guid.NewGuid().ToString());
            });
    }
}
