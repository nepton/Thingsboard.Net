using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

[Collection(nameof(TbTestCollection))]
public class UnassignAssetFromCustomerTester
{
    private readonly TbTestFixture _fixture;

    public UnassignAssetFromCustomerTester(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestUnassignAssetToCustomer()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        await client.AssignAssetToCustomerAsync(_fixture.CustomerId, newAsset.Id.Id);
        var actualAssigned = await client.GetAssetByIdAsync(newAsset.Id.Id);
        await client.UnassignAssetFromCustomerAsync(newAsset.Id.Id);
        var actualUnassigned = await client.GetAssetByIdAsync(newAsset.Id.Id);

        // assert
        Assert.NotNull(actualAssigned);
        Assert.NotNull(actualUnassigned);
        Assert.Equal(_fixture.CustomerId, actualAssigned.CustomerId!.Id);
        Assert.NotEqual(_fixture.CustomerId, actualUnassigned.CustomerId!.Id);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id.Id);
    }

    [Fact]
    public async Task TestUnassignAssetToCustomerThatDoesNotExists()
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
