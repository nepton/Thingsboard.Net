using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceClient;

/// <summary>
/// This class is used to test the deleteDevice method of the TbDeviceClient class.
/// 
/// We test following scenarios:
/// 1: Delete an device that exists.
/// 2: Delete an device that does not exist.
/// </summary>
[Collection(nameof(TbTestCollection))]
public class DeleteDeviceTests
{
    private readonly TbTestFixture _fixture;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public DeleteDeviceTests(TbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteExistsDeviceTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // Act
        var newEntity          = await DeviceUtility.CreateDeviceAsync(_fixture.DeviceProfileId);
        var entityBeforeDelete = await client.GetDeviceByIdAsync(newEntity.Id.Id);
        var exception          = await Record.ExceptionAsync(async () => await client.DeleteDeviceAsync(newEntity.Id.Id));
        var entityAfterDelete  = await client.GetDeviceByIdAsync(newEntity.Id.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsDeviceTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateDeviceClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteDeviceAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.DeleteDeviceAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(TbTestFactory.Instance.CreateDeviceClient(),
            async client =>
            {
                await client.DeleteDeviceAsync(Guid.NewGuid());
            });
    }
}
