using Thingsboard.Net;
using Thingsboard.Net.Exceptions;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the Clear Alarm API
/// 
/// We test the following scenarios:
/// 1: Clear an alarm
/// 2: Clear an alarm that already cleared
/// 3: Clear an alarm with an invalid alarm id
/// </summary>
public class ClearAlarmTests
{
    /// <summary>
    /// Clear an alarm
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestClearAlarm()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateNewAlarmAsync();

        // act
        await client.ClearAlarmAsync(newAlarm.Id!.Id);
        var clearedAlarm = await client.GetAlarmByIdAsync(newAlarm.Id!.Id);

        // assert
        Assert.NotNull(clearedAlarm);
        Assert.False(newAlarm.Cleared());
        Assert.True(clearedAlarm!.Cleared());

        // cleanup
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    /// <summary>
    /// Clear an alarm that already cleared
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestClearedAlarm()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateNewAlarmAsync();

        // act
        await client.ClearAlarmAsync(newAlarm.Id!.Id);
        await client.ClearAlarmAsync(newAlarm.Id!.Id);
        var clearedAlarm = await client.GetAlarmByIdAsync(newAlarm.Id!.Id);

        // assert
        Assert.NotNull(clearedAlarm);
        Assert.False(newAlarm.Cleared());
        Assert.True(clearedAlarm!.Cleared());

        // cleanup
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    [Fact]
    public async Task TestClearAlarmWithInvalidId()
    {
        // arrange
        var client    = TbTestFactory.Instance.CreateAlarmClient();
        var tbAlarmId = Guid.NewGuid();

        // act
        var exception = await Assert.ThrowsAsync<TbEntityNotFoundException>(
            async () =>
            {
                await client.ClearAlarmAsync(tbAlarmId);
            });

        // assert
        Assert.Equal(new TbEntityId(TbEntityType.ALARM, tbAlarmId), exception.EntityId);
    }
}
