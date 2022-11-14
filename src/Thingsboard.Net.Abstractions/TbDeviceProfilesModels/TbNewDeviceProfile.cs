using System;

namespace Thingsboard.Net;

public class TbNewDeviceProfile
{
    public TbEntityId?          TenantId           { get; set; }
    public string?              Name               { get; set; }
    public bool                 Default            { get; set; }
    public TbEntityId?          DefaultDashboardId { get; set; }
    public TbEntityId?          DefaultRuleChainId { get; set; }
    public string?              DefaultQueueName   { get; set; }
    public TbEntityId?          FirmwareId         { get; set; }
    public TbEntityId?          SoftwareId         { get; set; }
    public string?              Description        { get; set; }
    public object?              Image              { get; set; }
    public string?              ProvisionDeviceKey { get; set; }
    public string?              TransportType      { get; set; }
    public string?              ProvisionType      { get; set; }
    public TbDeviceProfileData? ProfileData        { get; set; }
    public string?              Type               { get; set; }
}
