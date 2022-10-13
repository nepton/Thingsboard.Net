using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the deleteAlarm method of the TbAlarmClient class.
/// 
/// We test following scenarios:
/// 1: Delete an alarm that exists.
/// 2: Delete an alarm that does not exist.
/// </summary>
public class DeleteAlarmTests
{
    [Fact]
    public async Task DeleteExistsAlarmTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        var newEntity          = await AlarmUtility.CreateAlarmAsync();
        var entityBeforeDelete = await client.GetAlarmByIdAsync(newEntity.Id!.Id);
        var exception          = await Record.ExceptionAsync(async () => await client.DeleteAlarmAsync(newEntity.Id!.Id));
        var entityAfterDelete  = await client.GetAlarmByIdAsync(newEntity.Id!.Id);

        // Assert
        Assert.NotNull(entityBeforeDelete);
        Assert.Null(entityAfterDelete);
        Assert.Null(exception);
    }

    [Fact]
    public async Task DeleteNotExistsAlarmTest()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        var ex = await Record.ExceptionAsync(async () => await client.DeleteAlarmAsync(Guid.NewGuid()));

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }
}
