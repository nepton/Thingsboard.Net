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
public class SaveNewDeviceProfileTests
{
    /// <summary>
    /// Save new deviceProfile with a valid deviceProfile object.
    /// </summary>
    [Fact]
    public async Task TestSaveNewDeviceProfile()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var deviceProfile    = DeviceProfileUtility.GenerateEntity();
        var newDeviceProfile = await client.SaveDeviceProfileAsync(deviceProfile);

        // assert
        Assert.NotNull(newDeviceProfile);
        Assert.NotNull(newDeviceProfile.Id);
        Assert.Equal(deviceProfile.Name, newDeviceProfile.Name);

        // cleanup
        await client.DeleteDeviceProfileAsync(newDeviceProfile.Id!.Id);
    }

    /// <summary>
    /// Save new deviceProfile with an invalid deviceProfile object.
    /// </summary>
    [Fact]
    public async Task TestSaveInvalidDeviceProfile()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // act
        var deviceProfile = DeviceProfileUtility.GenerateEntity();
        deviceProfile.Name = null;
        var ex = await Assert.ThrowsAsync<TbHttpException>(async () => await client.SaveDeviceProfileAsync(deviceProfile));

        // assert
        Assert.NotNull(ex);
        Assert.Equal(HttpStatusCode.BadRequest,                 ex.StatusCode);
        Assert.Equal("DeviceProfile name should be specified!", ex.Message);
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
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await client.SaveDeviceProfileAsync(default(TbNewDeviceProfile)!));

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
