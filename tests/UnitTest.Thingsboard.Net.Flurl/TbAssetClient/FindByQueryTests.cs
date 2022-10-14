using Thingsboard.Net;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// This class is used to test change password functionality
/// </summary>
public class FindByQueryTests
{
    // [Fact]
    // public async Task TestFindByQuery()
    // {
    //     // arrange
    //     var client = TbTestFactory.Instance.CreateAssetClient();
    //     var asset  = await AssetUtility.CreateAssetAsync();
    //
    //     // act
    //     var actual = await client.FindByQueryAsync(new TbAssetQueryRequest
    //     {
    //     });
    //
    //     // assert
    //     Assert.NotNull(actual);
    //     Assert.NotEmpty(actual);
    // }

    [Fact]
    public async Task TestFindEntityDataByQueryWhenEmpty()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = await client.FindByQueryAsync(new TbAssetQueryRequest
        {
            AssetTypes = new string[] {Guid.NewGuid().ToString()}
        });

        // assert
        Assert.NotNull(actual);
        Assert.Empty(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.FindByQueryAsync(new TbAssetQueryRequest
                {
                });
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.FindByQueryAsync(new TbAssetQueryRequest
                {
                });
            });
    }
}
