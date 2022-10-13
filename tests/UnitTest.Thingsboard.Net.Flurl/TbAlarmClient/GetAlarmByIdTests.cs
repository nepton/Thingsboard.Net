using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test GetAlarmByIdAsync API
/// We should do the test in the following items:
/// 1. Get the correct alarm id
/// 2. Get the incorrect alarm id
/// </summary>
public class GetAlarmByIdTests
{
    /// <summary>
    /// This test is used to test GetAlarmByIdAsync API with correct alarm id
    /// </summary>
    [Fact]
    public async Task TestGetAlarmByIdAsync()
    {
        //Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        // 1. Create a new alarm
        var alarm = await client.SaveAlarmAsync(new TbAlarm
        {
            Type = "TestAlarm",
            Originator = new TbEntityId
            {
                Id         = TbTestData.TestDeviceId,
                EntityType = TbEntityType.DEVICE
            },
            Severity = TbAlarmSeverity.CRITICAL,
            Status   = TbAlarmStatus.ACTIVE_UNACK,
            Details = new()
            {
                {"key1", "value1"},
                {"key2", "value2"}
            }
        });
        var newAlarm = await client.SaveAlarmAsync(alarm);
        var alarmId  = newAlarm.Id;
        Assert.NotNull(alarmId);
        var result = await client.GetAlarmByIdAsync(alarmId!.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(alarmId,          result!.Id);
        Assert.Equal(alarm.Type,       result.Type);
        Assert.Equal(alarm.Originator, result.Originator);
        Assert.Equal(alarm.Severity,   result.Severity);
        Assert.Equal(alarm.Status,     result.Status);
        Assert.Equal(alarm.Details,    result.Details);

        // 2. Delete the alarm
        await client.DeleteAlarmAsync(alarmId.Id);
    }

    [Fact]
    public async Task TestGetIncorrectAlarmByIdAsync()
    {
        //Arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // Act
        var result = await client.GetAlarmByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}
