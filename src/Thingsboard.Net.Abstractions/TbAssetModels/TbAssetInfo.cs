using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbAssetInfo
{
    public Dictionary<string, object?>? AdditionalInfo { get; set; }

    public TbEntityId? AssetProfileId { get; set; }

    public DateTime CreatedTime { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public bool CustomerIsPublic { get; set; }

    public string? CustomerTitle { get; set; }

    public TbEntityId? Id { get; set; }

    public string? Label { get; set; }

    public string? Name { get; set; }

    public TbEntityId? TenantId { get; set; }

    public string? Type { get; set; }
}