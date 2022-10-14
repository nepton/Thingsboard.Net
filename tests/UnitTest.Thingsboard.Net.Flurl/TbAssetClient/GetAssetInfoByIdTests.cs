using Newtonsoft.Json;
using Quibble.Xunit;
using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class GetAssetInfoByIdTests
{
    [Fact]
    public async Task TestGetAssetInfoById()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var expected = await AssetUtility.CreateAssetAsync();

        // act
        var actual = await client.GetAssetInfoByIdAsync(expected.Id.Id);

        Assert.NotNull(actual);
        Assert.Equal(expected.Id, actual.Id);

        // cleanup
        await client.DeleteAssetAsync(expected.Id!.Id);
    }

    [Fact]
    public async Task TestWhenAssetNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetAssetInfoByIdAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetInfoByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetAssetInfoByIdAsync(Guid.NewGuid());
            });
    }
}
