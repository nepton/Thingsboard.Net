using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetTenantDeviceByNameTests
{
    [Fact]
    public async Task TestGetTenantDeviceByName()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDeviceByNameAsync(TbTestData.TestDeviceName);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(TbTestData.GetTestDeviceId(), actual.Id.Id);
        Assert.Equal(TbTestData.TestDeviceName,    actual.Name);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDeviceByNameAsync(string.Empty);

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDeviceByNameAsync(string.Empty);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDeviceByNameAsync(string.Empty);
            });
    }
}
