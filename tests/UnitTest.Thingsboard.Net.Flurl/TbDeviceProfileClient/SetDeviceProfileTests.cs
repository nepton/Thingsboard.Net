using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

/// <summary>
/// This class is used to test the deleteDeviceProfile method of the TbDeviceProfileClient class.
/// 
/// We test following scenarios:
/// 1: Delete an deviceProfile that exists.
/// 2: Delete an deviceProfile that does not exist.
/// </summary>
public class SetDeviceProfileTests
{
    [Fact]
    public async Task SetExistsDeviceProfileTest()
    {
        // Arrange
        var client               = TbTestFactory.Instance.CreateDeviceProfileClient();
        var defaultDeviceProfile = await client.GetDefaultDeviceProfileInfoAsync();
        Assert.NotNull(defaultDeviceProfile);

        // Act
        var newEntity      = await DeviceProfileUtility.CreateDeviceProfileAsync();
        var exception      = await Record.ExceptionAsync(async () => await client.SetDefaultDeviceProfileAsync(newEntity.Id.Id));
        var entityAfterSet = await client.GetDefaultDeviceProfileInfoAsync();

        // Assert
        Assert.Null(entityAfterSet);
        Assert.Equal(newEntity.Id, entityAfterSet!.Id);
        Assert.Null(exception);

        // cleanup
        await client.SetDefaultDeviceProfileAsync(defaultDeviceProfile.Id.Id);
    }

    [Fact]
    public async Task SetNotExistsDeviceProfileTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.SetDefaultDeviceProfileAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.DeleteDeviceProfileAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceProfileClient(),
            async client =>
            {
                await client.DeleteDeviceProfileAsync(Guid.NewGuid());
            });
    }
}
