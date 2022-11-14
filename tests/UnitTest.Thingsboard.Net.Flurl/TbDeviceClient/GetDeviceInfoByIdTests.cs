using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

[Collection(nameof(TbTestCollection))]
public class GetDeviceInfoByIdTests
{
    private readonly TbTestFixture _fixture;

    public GetDeviceInfoByIdTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task TestGetDeviceInfoById()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceInfo = await client.GetDeviceInfoByIdAsync(_fixture.DeviceId);

        Assert.NotNull(deviceInfo);
        Assert.Equal(_fixture.DeviceId,    deviceInfo.Id.Id);
        Assert.Equal(_fixture.Device.Name, deviceInfo.Name);
    }

    [Fact]
    public async Task TestWhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var deviceId   = Guid.Empty;
        var deviceInfo = await client.GetDeviceInfoByIdAsync(deviceId);

        Assert.Null(deviceInfo);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceInfoByIdAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.GetDeviceInfoByIdAsync(Guid.NewGuid());
            });
    }
}
