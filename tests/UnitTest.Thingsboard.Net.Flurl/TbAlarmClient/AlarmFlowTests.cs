using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

public class AlarmFlowTests
{
    [Fact]
    public async Task TestCompletedFlow()
    {
        // create a new alarm
        var newAlarm = await CreateAlarmAsync();
        Assert.NotNull(newAlarm.Id);

        // get the alarm by id
        var alarm = await GetAlarmAsync(newAlarm.Id!);
        Assert.NotNull(alarm);
        Assert.Equal(newAlarm.Id, alarm.Id);

        // delete the alarm by id
        await DeleteAlarmAsync(newAlarm.Id!);
    }

    private async Task DeleteAlarmAsync(TbEntityId alarmId)
    {
        var client = TbTestFactory.Instance.CreateAlarmClient();
        await client.DeleteAlarmAsync(alarmId.Id);
    }

    private async Task<TbAlarm> GetAlarmAsync(TbEntityId newAlarmId)
    {
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm  = await client.GetAlarmByIdAsync(newAlarmId.Id);
        Assert.NotNull(alarm);

        return alarm!;
    }

    private async Task<TbAlarm> CreateAlarmAsync()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm = new TbAlarm
        {
            Id                     = null,
            TenantId               = null,
            CustomerId             = null,
            CreatedTime            = DateTime.Now,
            Name                   = "Test alarm",
            Type                   = "Test alarm type",
            Originator             = new TbEntityId(TbEntityType.DEVICE, TbTestData.TestDeviceId),
            Severity               = TbAlarmSeverity.CRITICAL,
            Status                 = TbAlarmStatus.ACTIVE_UNACK,
            StartTs                = DateTime.Now,
            EndTs                  = null,
            AckTs                  = null,
            ClearTs                = null,
            Details                = new Dictionary<string, object>() {["prop"] = "value"},
            Propagate              = false,
            PropagateToOwner       = false,
            PropagateToTenant      = false,
            PropagateRelationTypes = null,
        };

        // act
        var newAlarm = await client.SaveAlarmAsync(alarm);

        // assert
        Assert.NotNull(newAlarm);

        return newAlarm;
    }

    [Fact]
    public async Task WhenDeviceNotFound()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();

        // act
        var actual = await client.GetAlarmByIdAsync(Guid.NewGuid());

        // assert
        Assert.Null(actual);
    }
}
