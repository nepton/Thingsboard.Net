using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

public class UnassignAssetFromCustomerTester
{
    [Fact]
    public async Task TestUnassignAssetToCustomer()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        await client.AssignAssetToCustomerAsync(newAsset.Id!.Id, TbTestData.TestCustomerId);
        var actualAssigned = await client.GetAssetByIdAsync(newAsset.Id!.Id);
        await client.UnassignAssetFromCustomerAsync(newAsset.Id!.Id);
        var actualUnassigned = await client.GetAssetByIdAsync(newAsset.Id!.Id);

        // assert
        Assert.NotNull(actualAssigned);
        Assert.NotNull(actualUnassigned);
        Assert.Equal(TbTestData.TestCustomerId, actualAssigned!.CustomerId!.Id);
        Assert.NotEqual(TbTestData.TestCustomerId, actualAssigned!.CustomerId!.Id);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id!.Id);
    }

    [Fact]
    public async Task TestAssignAssetToCustomerThatDoesNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.UnassignAssetFromCustomerAsync(Guid.NewGuid());
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.UnassignAssetFromCustomerAsync(Guid.Empty);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateAssetClient(),
            async client =>
            {
                await client.UnassignAssetFromCustomerAsync(Guid.Empty);
            });
    }
}
