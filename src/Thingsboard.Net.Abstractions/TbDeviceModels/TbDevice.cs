using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

/// <summary>
/// Device object
/// </summary>
public class TbDevice
{
    public TbDevice(TbEntityId id)
    {
        Id = id;
    }

    /// <summary>
    /// Device Id
    /// </summary>
    public TbEntityId Id { get; }

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

    /// <summary>
    /// optional, defines additional infos for the device
    /// </summary>
    public Dictionary<string, object?> AdditionalInfo { get; } = new();

    /// <summary>
    /// Specifies whether the device is a gateway or not.
    /// </summary>
    public bool IsGateway
    {
        get => AdditionalInfo.TryGetValue("gateway", out var value) == true && value is true;
        set => AdditionalInfo["gateway"] = value;
    }
}
