using Newtonsoft.Json;
using Quibble.Xunit;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class GetTenantAssetByNameTests
{
    [Fact]
    public async Task TestGetTenantAssetByName()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var expected = await AssetUtility.CreateAssetAsync();
        Assert.NotNull(expected);

        // act
        var actual = await client.GetTenantAssetAsync(expected.Name);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(expected.Name, actual.Name);

        // cleanup
        await AssetUtility.DeleteAssetAsync(expected.Id);
    }

    [Fact]
    public async Task TestWhenAssetNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.GetTenantAssetAsync(string.Empty);

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetTenantAssetAsync(string.Empty);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.GetTenantAssetAsync(string.Empty);
            });
    }
}
