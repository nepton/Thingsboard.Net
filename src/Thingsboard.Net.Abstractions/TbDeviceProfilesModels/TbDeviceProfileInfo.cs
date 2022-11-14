namespace Thingsboard.Net;

public class TbDeviceProfileInfo
{
    public TbDeviceProfileInfo(TbEntityId id)
    {
        Id = id;
    }

    public TbEntityId                    Id                 { get; }
    public string?                       Name               { get; set; }
    public object?                       Image              { get; set; }
    public object?                       DefaultDashboardId { get; set; }
    public string?                       Type               { get; set; }
    public TbDeviceProfileTransportType? TransportType      { get; set; }
}
