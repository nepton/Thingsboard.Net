using Thingsboard.Net;

namespace UnitTest.Thingsboard.Net.Flurl.TbDeviceProfileClient;

public class DeviceProfileUtility
{
    public static TbNewDeviceProfile GenerateEntity()
    {
        var deviceProfile = new TbNewDeviceProfile
        {
            Name               = Guid.NewGuid().ToString(),
            Default            = false,
            DefaultDashboardId = null,
            DefaultRuleChainId = null,
            DefaultQueueName   = null,
            FirmwareId         = null,
            SoftwareId         = null,
            Description        = null,
            Image              = null,
            ProvisionDeviceKey = null,
            TransportType      = null,
            ProvisionType      = null,
            ProfileData        = null,
            Type               = "Test DeviceProfile Type",
        };

        return deviceProfile;
    }

    public static async Task<TbDeviceProfile> CreateDeviceProfileAsync()
    {
        var client        = TbTestFactory.Instance.CreateDeviceProfileClient();
        var deviceProfile = GenerateEntity();
        return await client.SaveDeviceProfileAsync(deviceProfile);
    }
}
