using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetDeviceByIdTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public GetDeviceByIdTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetDeviceById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId = _fixture.DeviceId;
        var actual   = await client.GetDeviceByIdAsync(deviceId);

        Assert.NotNull(actual);
        Assert.Equal(deviceId,             actual.Id.Id);
        Assert.Equal(_fixture.Device.Name, actual.Name);
    }

    [Fact]
    public async Task TestGetDeviceByIdWhenNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var actual = await client.GetDeviceByIdAsync(Guid.NewGuid());

        Assert.Null(actual);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceByIdAsync(Guid.NewGuid());
            });
    }
}
