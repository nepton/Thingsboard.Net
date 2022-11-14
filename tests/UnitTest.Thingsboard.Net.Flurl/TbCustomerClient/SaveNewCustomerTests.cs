using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

/// <summary>
/// This class is used to test the saveCustomer methods of the TbCustomerClient class.
/// </summary>
public class SaveNewCustomerTests
{
    /// <summary>
    /// Save new device with a valid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveNewCustomer()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var expected    = CustomerUtility.GenerateNewCustomer();
        var newCustomer = await client.SaveCustomerAsync(expected);

        // assert
        Assert.NotNull(newCustomer);
        Assert.NotNull(newCustomer.Id);
        Assert.Equal(expected.Address, newCustomer.Address);

        // cleanup
        await client.DeleteCustomerAsync(newCustomer.Id.Id);
    }

    /// <summary>
    /// Save new device with an invalid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidCustomer()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var actual = CustomerUtility.GenerateNewCustomer();
        actual.Email = "1234";
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveCustomerAsync(actual));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,              ex.StatusCode);
        Assert.Equal("Invalid email address format '1234'!", ex.Message);
    }

    /// <summary>
    /// Save new device with an invalid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullCustomer()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveCustomerAsync(default(TbNewCustomer)!));

        // assert
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.SaveCustomerAsync(CustomerUtility.GenerateNewCustomer());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.SaveCustomerAsync(CustomerUtility.GenerateNewCustomer());
            });
    }
}
