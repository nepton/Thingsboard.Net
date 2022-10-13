using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbAlarmClient;

public class AlarmUtility
{
    public static async Task<TbAlarm> CreateAlarmAsync()
    {
        // arrange
        var client = TbTestFactory.Instance.CreateAlarmClient();
        var alarm  = GenerateEntity();

        // act
        return await client.SaveAlarmAsync(alarm);
    }

    public static TbAlarm GenerateEntity()
    {
        var alarm = new TbAlarm
        {
            CreatedTime       = DateTime.Now,
            Name              = Guid.NewGuid().ToString(),
            Type              = Guid.NewGuid().ToString(),
            Originator        = new TbEntityId(TbEntityType.DEVICE, TbTestData.TestDeviceId),
            Severity          = TbAlarmSeverity.CRITICAL,
            Status            = TbAlarmStatus.ACTIVE_UNACK,
            Details           = new() {["prop"] = Guid.NewGuid().ToString()},
            Propagate         = false,
            PropagateToOwner  = false,
            PropagateToTenant = false,
        };
        return alarm;
    }
}
