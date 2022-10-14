using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// This class is used to test the saveAsset methods of the TbAssetClient class.
/// We will following scenarios:
/// 1. Save new Asset with a valid Asset object.
/// 2. Save new Asset with an invalid Asset object.
/// 3. Save new Asset with a null Asset object.
/// 4. Update an exists Asset with a valid Asset object.
/// 5. Update an not exists Asset with a valid Asset object.
/// </summary>
public class SaveAssetTests
{
    /// <summary>
    /// Save new Asset with a valid Asset object.
    /// </summary>
    [Fact]
    public async Task TestSaveNewAsset()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var expected    = AssetUtility.GenerateAsset();
        var actual = await client.SaveAssetAsync(expected);

        // assert
        Assert.NotNull(actual);
        Assert.NotNull(actual.Id);
        Assert.Equal(expected.Name,  actual.Name);
        Assert.Equal(expected.Label, actual.Label);

        // cleanup
        await client.DeleteAssetAsync(actual.Id!.Id);
    }

    /// <summary>
    /// Save new Asset with an invalid Asset object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidAsset()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var actual = AssetUtility.GenerateAsset();
        actual.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveAssetAsync(actual));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,          ex.StatusCode);
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
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveAssetAsync(null!));

        // assert
        Assert.NotNull(ex);
    }

    /// <summary>
    /// Update an exists Asset with a valid Asset object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestUpdateAssetAsync()
    {
        // arrange
        var client    = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        newAsset.Label = Guid.NewGuid().ToString();
        var updatedAsset = await client.SaveAssetAsync(newAsset);

        // assert
        Assert.NotNull(updatedAsset);
        Assert.Equal(newAsset.Label, updatedAsset.Label);

        // cleanup
        await client.DeleteAssetAsync(updatedAsset.Id!.Id);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.SaveAssetAsync(AssetUtility.GenerateAsset());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.SaveAssetAsync(AssetUtility.GenerateAsset());
            });
    }
}
