using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbCustomerClient;

public class GetCustomerShortInfoTests : IClassFixture<TbTestFixture>
{
    private readonly TbTestFixture _fixture;

    public GetCustomerShortInfoTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetCustomerShortInfo()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var actual = await client.GetCustomerShortInfoAsync(_fixture.CustomerId);

        Assert.NotNull(actual);
    }

    [Fact]
    public async Task TestWhenCustomerNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateCustomerClient();

        // act
        var customerId = Guid.Empty;
        var actual     = await client.GetCustomerShortInfoAsync(customerId);

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetCustomerShortInfoAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateCustomerClient(),
            async client =>
            {
                await client.GetCustomerShortInfoAsync(Guid.NewGuid());
            });
    }
}
