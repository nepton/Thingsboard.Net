using System;

namespace Thingsboard.Net;

/// <summary>
/// Device object
/// </summary>
public class TbNewDevice
{
    public TbEntityId? TenantId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Label { get; set; }

    public TbEntityId? DeviceProfileId { get; set; }

    public TbEntityId? FirmwareId { get; set; }

    public TbEntityId? SoftwareId { get; set; }
}
