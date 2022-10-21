using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// Test assign asset to customer.
/// </summary>
public class AssignAssetToCustomerTests
{
    [Fact]
    public async Task TestAssignAssetToCustomer()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        await client.AssignAssetToCustomerAsync(newAsset.Id!.Id, TbTestData.TestCustomerId);
        var actual = await client.GetAssetByIdAsync(newAsset.Id!.Id);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(TbTestData.TestCustomerId, actual!.CustomerId!.Id);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id!.Id);
    }

    [Fact]
    public async Task TestAssignAssetToCustomerThatDoesNotExists()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.AssignAssetToCustomerAsync(newAsset.Id!.Id, Guid.NewGuid());
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id!.Id);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.AssignAssetToCustomerAsync(Guid.Empty, Guid.NewGuid());
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
