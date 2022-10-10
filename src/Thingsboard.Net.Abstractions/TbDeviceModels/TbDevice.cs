using System;

namespace Thingsboard.Net;

/// <summary>
/// Device object
/// </summary>
public class TbDevice
{
    /// <summary>
    /// Device Id
    /// </summary>
    public TbEntityId? Id { get; set; }

    /// <summary>
    /// Created time in js timestamp format.
    /// </summary>
    public DateTime CreatedTime { get; set; }

    public TbEntityId? TenantId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Label { get; set; }

    public TbEntityId? DeviceProfileId { get; set; }

    public TbEntityId? FirmwareId { get; set; }

    public TbEntityId? SoftwareId { get; set; }
}
