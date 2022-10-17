using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// This class is used to test the deleteAsset method of the TbAssetClient class.
/// 
/// We test following scenarios:
/// 1: Delete an device that exists.
/// 2: Delete an device that does not exist.
/// </summary>
public class DeleteAssetTests
{
    [Fact]
    public async Task DeleteExistsAssetTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // Act
        var newEntity          = await AssetUtility.CreateAssetAsync();
        var entityBeforeDelete = await client.GetAssetByIdAsync(newEntity.Id!.Id);
        var exception          = await Record.ExceptionAsync(async () => await client.DeleteAssetAsync(newEntity.Id!.Id));
        var entityAfterDelete  = await client.GetAssetByIdAsync(newEntity.Id!.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsAssetTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteAssetAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.DeleteAssetAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.DeleteAssetAsync(Guid.NewGuid());
            });
    }
}
