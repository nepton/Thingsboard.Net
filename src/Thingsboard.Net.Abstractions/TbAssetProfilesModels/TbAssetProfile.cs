using System;

namespace Thingsboard.Net;

public class TbAssetProfile
{
    public TbAssetProfile(TbEntityId id)
    {
        Id = id;
    }

    public DateTime CreatedTime { get; set; }

    public bool Default { get; set; }

    public TbEntityId? DefaultDashboardId { get; set; }

    public string? DefaultQueueName { get; set; }

    public TbEntityId? DefaultRuleChainId { get; set; }

    public string? Description { get; set; }

    public TbEntityId Id { get; }

    public object? Image { get; set; }

    public string? Name { get; set; }

    public TbEntityId? TenantId { get; set; }

    public string? Type { get; set; }
}