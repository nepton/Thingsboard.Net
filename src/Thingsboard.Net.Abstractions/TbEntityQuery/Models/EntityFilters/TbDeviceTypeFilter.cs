namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter devices based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Temperature Sensor' devices which name starts with 'ABC':
/// </summary>
public class TbDeviceTypeFilter : TbEntityFilter
{
    public override string Type => "deviceType";

    public string? DeviceType { get; set; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? DeviceNameFilter { get; set; }

    public TbDeviceTypeFilter()
    {
    }

    public TbDeviceTypeFilter(string deviceType, string? deviceNameStartsWith)
    {
        DeviceType       = deviceType;
        DeviceNameFilter = deviceNameStartsWith;
    }
}
