using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

/// <summary>
/// This class is used to test the GetAlarms method of <see cref="TbAlarmClient"/> class.
/// We will following scenarios:
/// 1. Get all alarms with limit.
/// 2. Get nothing has right response.
/// </summary>
public class GetAlarmsTests
{
    [Fact]
    public async Task TestGetAlarms()
    {
        // arrange
        var client   = TbTestFactory.Instance.CreateAlarmClient();
        var newAlarm = await AlarmUtility.CreateAlarmAsync();

        // act
        var alarms = await client.GetAlarmsAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, 20, 0, textSearch: newAlarm.Name);

        // assert
        Assert.NotNull(alarms);
        Assert.NotEmpty(alarms.Data);
        Assert.Equal(1, alarms.TotalElements);
        Assert.Single(alarms.Data);
        Assert.Equal(newAlarm.Name, alarms.Data[0].Name);

        // clean up
        await client.DeleteAlarmAsync(newAlarm.Id!.Id);
    }

    [Fact]
    public async Task TestGetNothing()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var alarms = await client.GetAlarmsAsync(TbEntityType.DEVICE, TbTestData.TestDeviceId, 20, 0, textSearch: Guid.NewGuid().ToString());

        // assert
        Assert.NotNull(alarms);
        Assert.Empty(alarms.Data);
        Assert.Equal(0, alarms.TotalElements);
    }
}
