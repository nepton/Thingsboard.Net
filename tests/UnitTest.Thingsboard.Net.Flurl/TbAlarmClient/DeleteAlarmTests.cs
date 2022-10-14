using Thingsboard.Net.Exceptions;
using UnitTest.Thingsboard.Net.Flurl.TbCommon;

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
    public async Task TestDeleteExistsAlarm()
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
    public async Task TestDeleteNotExistsAlarm()
    {
        // Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        var ex = await Record.ExceptionAsync(async () =>
        {
            await client.DeleteAlarmAsync(Guid.NewGuid());
        });

        // Assert
        Assert.IsType<TbEntityNotFoundException>(ex);
    }

    [Fact]
    public async Task TestIncorrectUsername()
    {
        await new TbCommonTestHelper().TestIncorrectUsername(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.DeleteAlarmAsync(Guid.NewGuid());
            });
    }

    [Fact]
    public async Task TestIncorrectBaseUrl()
    {
        await new TbCommonTestHelper().TestIncorrectBaseUrl(
            TbTestFactory.Instance.CreateAlarmClient(),
            async client =>
            {
                await client.DeleteAlarmAsync(Guid.NewGuid());
            });
    }
}
