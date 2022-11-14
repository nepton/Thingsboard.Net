using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// This class is used to test the saveAsset methods of the TbAssetClient class.
/// We will following scenarios:
/// * Update an exists Asset with a valid Asset object.
/// * Update an not exists Asset with a valid Asset object.
/// </summary>
public class SaveExistsAssetTests
{
    /// <summary>
    /// Update an exists Asset with a valid Asset object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestUpdateAssetAsync()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        newAsset.Label = Guid.NewGuid().ToString();
        var updatedAsset = await client.SaveAssetAsync(newAsset);

        // assert
        Assert.NotNull(updatedAsset);
        Assert.Equal(newAsset.Label, updatedAsset.Label);

        // cleanup
        await client.DeleteAssetAsync(updatedAsset.Id.Id);
    }

    /// <summary>
    /// Save new Asset with an invalid Asset object.
    /// </summary>
    [Fact]
    public async Task TestSaveWhenAssetNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();


        // act
        var asset = new TbAsset(new TbEntityId(TbEntityType.ASSET, Guid.NewGuid()));
        asset.Name = null;
        var ex = await Assert.ThrowsAsync<TbEntityNotFoundException>(async () => await client.SaveAssetAsync(asset));

        // assert
        Assert.NotNull(ex);
    }

    /// <summary>
    /// Save new Asset with an invalid Asset object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidAsset()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();
        var asset  = await AssetUtility.CreateAssetAsync();

        // act
        asset.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () =>
        {
            await client.SaveAssetAsync(asset);
        });

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,         ex.StatusCode);
        Assert.Equal("Asset name should be specified!", ex.Message);
    }

    /// <summary>
    /// Save new Asset with an invalid Asset object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullAsset()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveAssetAsync(default(TbAsset)!));

        // assert
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.SaveAssetAsync(AssetUtility.GenerateNewAsset());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.SaveAssetAsync(AssetUtility.GenerateNewAsset());
            });
    }
}
