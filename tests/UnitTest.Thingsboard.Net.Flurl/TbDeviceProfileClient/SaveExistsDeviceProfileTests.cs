using System.Net;
using Thingsboard.Net;
using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

/// <summary>
/// This class is used to test the saveDeviceProfile methods of the TbDeviceProfileClient class.
/// We will following scenarios:
/// 1. Save new deviceProfile with a valid deviceProfile object.
/// 2. Save new deviceProfile with an invalid deviceProfile object.
/// 3. Save new deviceProfile with a null deviceProfile object.
/// 4. Update an exists deviceProfile with a valid deviceProfile object.
/// 5. Update an not exists deviceProfile with a valid deviceProfile object.
/// </summary>
public class SaveExistsDeviceProfileTests
{
    /// <summary>
    /// Update an exists deviceProfile with a valid deviceProfile object.
    /// </summary>
    /// <returns></returns>
    [Fact]
    private async Task TestSaveExistsDeviceProfileAsync()
    {
        // arrange
        var client        = TbTestFactory.Instance.CreateDeviceProfileClient();
        var deviceProfile = await DeviceProfileUtility.CreateDeviceProfileAsync();

        // act
        deviceProfile.Description = Guid.NewGuid().ToString();

        var updatedDeviceProfile = await client.SaveDeviceProfileAsync(deviceProfile);

        // assert
        Assert.NotNull(updatedDeviceProfile);
        Assert.Equal(deviceProfile.Description, updatedDeviceProfile.Description);

        // cleanup
        await client.DeleteDeviceProfileAsync(updatedDeviceProfile.Id.Id);
    }

    /// <summary>
    /// Save new deviceProfile with an invalid deviceProfile object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidDeviceProfile()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();
        var actual = await DeviceProfileUtility.CreateDeviceProfileAsync();

        // act
        actual.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveDeviceProfileAsync(actual));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,                 ex.StatusCode);
        Assert.Equal("DeviceProfile name should be specified!", ex.Message);

        // clean up
        await Task.Delay(500);
        await client.DeleteDeviceProfileAsync(actual.Id.Id);
    }

    /// <summary>
    /// Save new deviceProfile with an invalid deviceProfile object.
    /// </summary>
    [Fact]
    public async Task TestSaveNullDeviceProfile()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveDeviceProfileAsync(default(TbDeviceProfile)!));

        // assert
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.SaveDeviceProfileAsync(DeviceProfileUtility.GenerateEntity());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.SaveDeviceProfileAsync(DeviceProfileUtility.GenerateEntity());
            });
    }
}
