using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

/// <summary>
/// This class is used to test the deleteCustomer method of the TbCustomerClient class.
/// 
/// We test following scenarios:
/// 1: Delete an customer that exists.
/// 2: Delete an customer that does not exist.
/// </summary>
public class DeleteCustomerTests
{
    [Fact]
    public async Task DeleteExistsCustomerTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // Act
        var newEntity          = await CustomerUtility.CreateEntityAsync();
        var entityBeforeDelete = await client.GetCustomerByIdAsync(newEntity.Id!.Id);
        var exception          = await Record.ExceptionAsync(async () => await client.DeleteCustomerAsync(newEntity.Id!.Id));
        var entityAfterDelete  = await client.GetCustomerByIdAsync(newEntity.Id!.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsCustomerTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteCustomerAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.DeleteCustomerAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.DeleteCustomerAsync(Guid.NewGuid());
            });
    }
}
