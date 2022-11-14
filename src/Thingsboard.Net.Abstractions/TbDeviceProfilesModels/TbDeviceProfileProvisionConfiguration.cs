namespace Thingsboard.Net;

public class TbDeviceProfileProvisionConfiguration
{
    public TbDeviceProfileProvisionType Type                  { get; set; }
    public string?                ProvisionDeviceKey    { get; set; }
    public string?                ProvisionDeviceSecret { get; set; }
}
