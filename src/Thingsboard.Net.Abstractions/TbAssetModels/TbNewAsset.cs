using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbNewAsset
{
    public Dictionary<string, object?>? AdditionalInfo { get; set; }

    public TbEntityId? AssetProfileId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public string? Label { get; set; }

    public string? Name { get; set; }

    public TbEntityId? TenantId { get; set; }

    public string? Type { get; set; }
}