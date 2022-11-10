using System.Collections.Generic;

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
