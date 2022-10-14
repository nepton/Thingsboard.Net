using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the Ack Alarm API
/// 
/// We test the following scenarios:
/// 1: Acknowledge an alarm
/// 2: Acknowledge an alarm that already acknowledged
/// 3: Acknowledge an alarm with an invalid alarm id
/// </summary>
public class AckAlarmTests
{
    /// <summary>
    /// Acknowledge an alarm
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestAckAlarm()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateNewAlarmAsync();

        // act
        await client.AcknowledgeAlarmAsync(newAlarm.Id!.Id);
        var ackedAlarm = await client.GetAlarmByIdAsync(newAlarm.Id!.Id);

        // assert
        Assert.NotNull(ackedAlarm);
        Assert.False(newAlarm.Acknowledged());
        Assert.True(ackedAlarm!.Acknowledged());

        // cleanup
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    /// <summary>
    /// Acknowledge an alarm that already acknowledged
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestAckedAlarm()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateNewAlarmAsync();

        // act
        await client.AcknowledgeAlarmAsync(newAlarm.Id!.Id);
        await client.AcknowledgeAlarmAsync(newAlarm.Id!.Id);
        var ackedAlarm = await client.GetAlarmByIdAsync(newAlarm.Id!.Id);

        // assert
        Assert.NotNull(ackedAlarm);
        Assert.False(newAlarm.Acknowledged());
        Assert.True(ackedAlarm!.Acknowledged());

        // cleanup
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    [Fact]
    public async Task TestAckAlarmWithInvalidId()
    {
        // arrange
        var client    = TbTestFactory.Instance.CreateAlarmClient();
        var tbAlarmId = Guid.NewGuid();

        // act
        var exception = await Assert.ThrowsAsync<TbEntityNotFoundException>(
            async () =>
            {
                await client.AcknowledgeAlarmAsync(tbAlarmId);
            });

        // assert
        Assert.Equal(new TbEntityId(TbEntityType.ALARM, tbAlarmId), exception.EntityId);
    }
}
