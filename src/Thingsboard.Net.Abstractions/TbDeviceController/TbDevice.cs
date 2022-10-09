using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbDeviceController;

/// <summary>
/// 设备信息
/// </summary>
public class TbDevice
{
    /// <summary>
    /// 电池组的Id
    /// </summary>
    public TbEntityId Id { get; }

    /// <summary>
    /// Created time in js timestamp format.
    /// </summary>
    public long CreatedTime { get; }

    public TbEntityId? TenantId { get; }

    public TbEntityId? CustomerId { get; }

    public string? Name { get; }

    public string? Type { get; }

    public string? Label { get; }

    public TbEntityId? DeviceProfileId { get; }

    public TbEntityId? FirmwareId { get; }

    public TbEntityId? SoftwareId { get; }

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbDevice(TbEntityId id,
        long                   createdTime,
        TbEntityId?            tenantId,
        TbEntityId?            customerId,
        string?                name,
        string?                type,
        string?                label,
        TbEntityId?            deviceProfileId,
        TbEntityId?            firmwareId,
        TbEntityId?            softwareId)
    {
        Id              = id;
        CreatedTime     = createdTime;
        TenantId        = tenantId;
        CustomerId      = customerId;
        Name            = name;
        Type            = type;
        Label           = label;
        DeviceProfileId = deviceProfileId;
        FirmwareId      = firmwareId;
        SoftwareId      = softwareId;
    }
}
