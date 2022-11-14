namespace Thingsboard.Net;

public class TbDeviceProfileData
{
    public TbDeviceProfileConfiguration?          Configuration          { get; set; }
    public TbDeviceProfileTransportConfiguration? TransportConfiguration { get; set; }
    public TbDeviceProfileProvisionConfiguration? ProvisionConfiguration { get; set; }
    public object[]?                        Alarms                 { get; set; }
}
