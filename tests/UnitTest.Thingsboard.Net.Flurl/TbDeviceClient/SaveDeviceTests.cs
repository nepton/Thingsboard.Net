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
public class SaveDeviceTests
{
    /// <summary>
    /// Save new device with a valid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveNewDevice()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var device    = DeviceUtility.GenerateEntity();
        var newDevice = await client.SaveDeviceAsync(device);

        // assert
        Assert.NotNull(newDevice);
        Assert.NotNull(newDevice.Id);
        Assert.Equal(device.Name,  newDevice.Name);
        Assert.Equal(device.Label, newDevice.Label);

        // cleanup
        await client.DeleteDeviceAsync(newDevice.Id!.Id);
    }

    /// <summary>
    /// Save new device with an invalid device object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidDevice()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // act
        var device = DeviceUtility.GenerateEntity();
        device.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveDeviceAsync(device));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,          ex.StatusCode);
        Assert.Equal("Device name should be specified!", ex.Message);
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
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveDeviceAsync(null!));

        // assert
        Assert.NotNull(ex);
    }

    /// <summary>
    /// Update an exists device with a valid device object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestUpdateDeviceAsync()
    {
        // arrange
        var client    = TbTestFactory.Instance.CreateDeviceClient();
        var newDevice = await DeviceUtility.CreateDeviceAsync();

        // act
        newDevice.Label = Guid.NewGuid().ToString();
        var updatedDevice = await client.SaveDeviceAsync(newDevice);

        // assert
        Assert.NotNull(updatedDevice);
        Assert.Equal(newDevice.Label, updatedDevice.Label);

        // cleanup
        await client.DeleteDeviceAsync(updatedDevice.Id!.Id);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.SaveDeviceAsync(DeviceUtility.GenerateEntity());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.SaveDeviceAsync(DeviceUtility.GenerateEntity());
            });
    }
}
