namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter devices based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'Temperature Sensor' devices which name starts with 'ABC':
/// </summary>
public class TbDeviceTypeFilter : TbEntityFilter
{
    public string DeviceType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? DeviceNameStartsWith { get; }

    public TbDeviceTypeFilter(string deviceType, string? deviceNameStartsWith)
    {
        DeviceType           = deviceType;
        DeviceNameStartsWith = deviceNameStartsWith;
    }

    public override object ToQuery()
    {
        return new
        {
            type             = "deviceType",
            deviceType       = DeviceType,
            deviceNameFilter = DeviceNameStartsWith,
        };
    }
}
