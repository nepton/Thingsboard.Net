using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

[Collection(nameof(TbTestCollection))]
public class GetTenantCustomerTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetTenantCustomerTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetTenantCustomer()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var actual = await client.GetTenantCustomerAsync(_fixture.Customer.Title!);

        Assert.NotNull(actual);
        Assert.Equal(_fixture.CustomerId, actual.Id.Id);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var customerId = Guid.Empty;
        var actual     = await client.GetTenantCustomerAsync(Guid.NewGuid().ToString());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetTenantCustomerAsync(_fixture.Customer.Title!);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetTenantCustomerAsync(_fixture.Customer.Title!);
            });
    }
}
