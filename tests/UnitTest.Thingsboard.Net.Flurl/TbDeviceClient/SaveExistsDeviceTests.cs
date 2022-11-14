using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

/// <summary>
/// This class is used to test the saveDevice methods of the TbDeviceClient class.
/// We will following scenarios:
/// 1. Save new device with a valid device object.
/// 2. Save new device with an invalid device object.
/// 3. Save new device with a null device object.
/// 4. Update an exists device with a valid device object.
/// 5. Update an not exists device with a valid device object.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class SaveExistsDeviceTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public SaveExistsDeviceTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Update an exists device with a valid device object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestSaveExistsDeviceAsync()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();
        var device = await DeviceUtility.CreateDeviceAsync(_fixture.DeviceProfileId);

        // act
        device.Label                       = Guid.NewGuid().ToString();
        device.AdditionalInfo["gateway"]   = true;
        device.AdditionalInfo["stringKey"] = "stringValue";

        var updatedDevice = await client.SaveDeviceAsync(device);

        // assert
        Assert.NotNull(updatedDevice);
        Assert.Equal(device.Label,          updatedDevice.Label);
        Assert.Equal(device.AdditionalInfo, updatedDevice.AdditionalInfo);

        // cleanup
        await client.DeleteDeviceAsync(updatedDevice.Id.Id);
    }

    /// <summary>
    /// Save new device with an invalid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidDevice()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();
        var actual = await DeviceUtility.CreateDeviceAsync(_fixture.DeviceProfileId);

        // act
        actual.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveDeviceAsync(actual));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,          ex.StatusCode);
        Assert.Equal("Device name should be specified!", ex.Message);

        // clean up
        await Task.Delay(500);
        await client.DeleteDeviceAsync(actual.Id.Id);
    }

    /// <summary>
    /// Save new device with an invalid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullDevice()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveDeviceAsync(default(TbDevice)!));

        // assert
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.SaveDeviceAsync(DeviceUtility.GenerateEntity(_fixture.DeviceProfileId));
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.SaveDeviceAsync(DeviceUtility.GenerateEntity(_fixture.DeviceProfileId));
            });
    }
}
