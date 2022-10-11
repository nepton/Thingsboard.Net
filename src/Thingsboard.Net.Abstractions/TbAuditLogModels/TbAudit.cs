using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

public class TbAudit
{
    public TbId? Id { get; set; }

    public DateTime? CreatedTime { get; set; }

    public TbEntityId? TenantId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public TbEntityId? EntityId { get; set; }

    public string? EntityName { get; set; }

    public TbEntityId? UserId { get; set; }

    public string? UserName { get; set; }

    public string? ActionType { get; set; }

    public string? ActionStatus { get; set; }

    public Dictionary<string, object>? ActionData { get; set; }

    public string? ActionFailureDetails { get; set; }
}
