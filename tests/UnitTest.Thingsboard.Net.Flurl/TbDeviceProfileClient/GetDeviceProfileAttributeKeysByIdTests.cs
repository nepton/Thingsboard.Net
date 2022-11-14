using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDeviceProfileAttributeKeysByIdTests
{
    [Fact]
    public async Task TestGetDeviceProfileById()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateDeviceProfileClient();
        var expected = await client.GetDefaultDeviceProfileInfoAsync();
        Assert.NotNull(expected);

        // act
        var actual = await client.GetAttributeKeysAsync(expected.Id.Id);

        Assert.NotNull(actual);
    }

    [Fact]
    public async Task TestGetDeviceProfileByIdWhenNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var actual = await client.GetAttributeKeysAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.GetDeviceProfileByIdAsync(Guid.NewGuid());
            });
    }
}
