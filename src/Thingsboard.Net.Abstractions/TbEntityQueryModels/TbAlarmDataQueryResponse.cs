using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Thingsboard.Net;

public class TbAlarmDataQueryResponse
{
    public TbEntityId Id { get; set; } = TbEntityId.Empty;

    public TbEntityId? EntityId { get; set; }

    public ReadOnlyDictionary<TbEntityFieldType, ReadOnlyDictionary<string, TbEntityTsValue>>? Latest { get; set; }

    public DateTime CreatedTime { get; set; }

    public TbEntityId? TenantId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public string Name { get; set; } = "";

    public string Type { get; set; } = "";

    public TbEntityId? Originator { get; set; }

    public TbAlarmSeverity Severity { get; set; }

    public bool Acknowledged { get; set; }

    public bool Cleared { get; set; }

    public TbEntityId? AssigneeId { get; set; }

    public DateTime? StartTs { get; set; }

    public DateTime? EndTs { get; set; }

    public DateTime? AckTs { get; set; }

    public DateTime? ClearTs { get; set; }

    public DateTime? AssignTs { get; set; }

    public Dictionary<string, object?>? Details { get; set; }

    public bool Propagate { get; set; }

    public string? OriginatorName { get; set; }

    public bool PropagateToOwner { get; set; }

    public string? OriginatorLabel { get; set; }

    public string? PropagateToTenant { get; set; }

    public TbAlarmAssignee? Assignee { get; set; }

    public string[]? PropagateRelationTypes { get; set; }

    public TbAlarmStatus Status { get; set; }
}
