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
public class DeleteDeviceProfileTests
{
    [Fact]
    public async Task DeleteExistsDeviceProfileTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // Act
        var newEntity          = await DeviceProfileUtility.CreateDeviceProfileAsync();
        var entityBeforeDelete = await client.GetDeviceProfileByIdAsync(newEntity.Id!.Id);
        var exception          = await Record.ExceptionAsync(async () => await client.DeleteDeviceProfileAsync(newEntity.Id!.Id));
        var entityAfterDelete  = await client.GetDeviceProfileByIdAsync(newEntity.Id!.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsDeviceProfileTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateDeviceProfileClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceProfileAsync(Guid.NewGuid());
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
