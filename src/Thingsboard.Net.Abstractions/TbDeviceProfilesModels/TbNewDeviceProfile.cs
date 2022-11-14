using System;

namespace Thingsboard.Net;

public class TbNewDeviceProfile
{
    public static TbDeviceProfileData DefaultProfileData => new()
    {
        Configuration = null,
        TransportConfiguration = new TbDeviceProfileTransportConfiguration
        {
            Type = "DEFAULT"
        },
        ProvisionConfiguration = new TbDeviceProfileProvisionConfiguration
        {
            Type = TbDeviceProfileProvisionType.DISABLED,
        },
        Alarms = Array.Empty<object>(),
    };

    public TbNewDeviceProfile() : this(DefaultProfileData)
    {
    }

    public TbNewDeviceProfile(Action<TbDeviceProfileData> action) : this(DefaultProfileData)
    {
        action(ProfileData);
    }

    public TbNewDeviceProfile(TbDeviceProfileData profileData)
    {
        ProfileData = profileData;
    }

    public string?                       Name               { get; set; }
    public bool                          Default            { get; set; }
    public TbEntityId?                   DefaultDashboardId { get; set; }
    public TbEntityId?                   DefaultRuleChainId { get; set; }
    public string?                       DefaultQueueName   { get; set; }
    public TbEntityId?                   FirmwareId         { get; set; }
    public TbEntityId?                   SoftwareId         { get; set; }
    public string?                       Description        { get; set; }
    public object?                       Image              { get; set; }
    public TbDeviceProfileProvisionType? ProvisionType      => ProfileData.ProvisionConfiguration?.Type ?? TbDeviceProfileProvisionType.DISABLED;
    public string?                       ProvisionDeviceKey => ProfileData.ProvisionConfiguration?.ProvisionDeviceKey;
    public string?                       TransportType      => ProfileData.TransportConfiguration?.Type;
    public TbDeviceProfileData           ProfileData        { get; }

    public string? Type { get; } = "DEFAULT";
}
