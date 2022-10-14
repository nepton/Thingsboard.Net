using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

public class GetDevicesByIdsTests
{
    [Fact]
    public async Task TestGetDevicesByIds()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDevicesByIdsAsync(new[] {TbTestData.TestDeviceId, TbTestData.TestDeviceId2});

        Assert.NotNull(actual);
        Assert.Equal(2, actual.Length);
        Assert.True(actual.Any(d => d.Id?.Id == TbTestData.TestDeviceId));
        Assert.True(actual.Any(d => d.Id?.Id == TbTestData.TestDeviceId2));
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDevicesByIdsAsync(new[] {Guid.Empty, Guid.NewGuid()});
        
        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDevicesByIdsAsync(new[] {TbTestData.TestDeviceId, TbTestData.TestDeviceId2});
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDevicesByIdsAsync(new[] {TbTestData.TestDeviceId, TbTestData.TestDeviceId2});
            });
    }
}
