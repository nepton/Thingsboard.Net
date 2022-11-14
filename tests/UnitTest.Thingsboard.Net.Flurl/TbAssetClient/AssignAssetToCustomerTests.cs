using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbAssetClient;

/// <summary>
/// Test assign asset to customer.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class AssignAssetToCustomerTests
{
    private readonly TbTestFixture _fixture;

    public AssignAssetToCustomerTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestAssignAssetToCustomer()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        await client.AssignAssetToCustomerAsync(_fixture.CustomerId, newAsset.Id.Id);
        var actual = await client.GetAssetByIdAsync(newAsset.Id.Id);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(_fixture.CustomerId, actual.CustomerId!.Id);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id.Id);
    }

    [Fact]
    public async Task TestAssignAssetToCustomerThatAssetDoesNotExists()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAssetClient();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.AssignAssetToCustomerAsync(_fixture.CustomerId, Guid.NewGuid());
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);

        var notFoundException = (TbEntityNotFoundException) ex;
        Assert.Equal(TbEntityType.ASSET, notFoundException.EntityId.EntityType);
    }

    [Fact]
    public async Task TestAssignAssetToCustomerThatCustomerDoesNotExists()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAssetClient();
        var newAsset = await AssetUtility.CreateAssetAsync();

        // act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.AssignAssetToCustomerAsync(Guid.NewGuid(), newAsset.Id.Id);
        });

        // assert
        Assert.NotNull(ex);
        Assert.IsType<TbEntityNotFoundException>(ex);

        var notFoundException = (TbEntityNotFoundException) ex;
        Assert.Equal(TbEntityType.CUSTOMER, notFoundException.EntityId.EntityType);

        // cleanup
        await client.DeleteAssetAsync(newAsset.Id.Id);
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
