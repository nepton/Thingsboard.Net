using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class GetDeviceProfileByIdTests
{
    [Fact]
    public async Task TestGetDeviceProfileById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var expected = await client.GetDefaultDeviceProfileInfoAsync();
        Assert.NotNull(expected);

        var actual = await client.GetDeviceProfileByIdAsync(expected.Id.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Id,   actual.Id);
        Assert.Equal(expected.Name, actual.Name);
    }

    [Fact]
    public async Task TestGetDeviceProfileByIdWhenNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var actual = await client.GetDeviceProfileByIdAsync(Guid.NewGuid());

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
