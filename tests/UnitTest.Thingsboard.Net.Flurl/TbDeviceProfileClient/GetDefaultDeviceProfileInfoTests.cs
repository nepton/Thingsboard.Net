using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDefaultDeviceProfileInfoTests
{
    [Fact]
    public async Task TestGetDefaultDeviceProfileInfo()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var expected = await client.GetDefaultDeviceProfileInfoAsync();

        // assert
        Assert.NotNull(expected);
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
