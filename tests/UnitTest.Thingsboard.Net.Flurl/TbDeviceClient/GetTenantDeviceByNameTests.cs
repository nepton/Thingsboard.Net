using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetTenantDeviceByNameTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetTenantDeviceByNameTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetTenantDeviceByName()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDeviceByNameAsync(_fixture.Device.Name!);

        // assert
        Assert.NotNull(actual);
        Assert.Equal(_fixture.DeviceId,     actual.Id.Id);
        Assert.Equal(_fixture.Device.Name!, actual.Name);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetTenantDeviceByNameAsync(string.Empty);

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDeviceByNameAsync(string.Empty);
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetTenantDeviceByNameAsync(string.Empty);
            });
    }
}
