using System;
using System.Collections.Generic;

namespace Thingsboard.Net;

/// <summary>
/// Alarm information object of the TB system
/// </summary>
public class TbNewAlarm
{
    public TbEntityId? TenantId { get; set; }

    public TbEntityId? CustomerId { get; set; }

    public string Name { get; set; } = "";

    /// <summary>
    /// type
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// The sender
    /// </summary>
    public TbEntityId? Originator { get; set; }

    /// <summary>
    /// severity
    /// </summary>
    public TbAlarmSeverity Severity { get; set; }

    public TbAlarmStatus Status { get; set; }

    /// <summary>
    /// The start time
    /// </summary>
    public DateTime? StartTs { get; set; }

    /// <summary>
    /// End time (this time is the time when the alarm information was last received)
    /// </summary>
    public DateTime? EndTs { get; set; }

    /// <summary>
    /// The time when the acknowledgement command was sent
    /// </summary>
    public DateTime? AckTs { get; set; }

    /// <summary>
    /// Time when the clearing command was sent
    /// </summary>
    public DateTime? ClearTs { get; set; }

    /// <summary>
    /// Details of the alarm information
    /// </summary>
    public Dictionary<string, object?> Details { get; set; } = new();

    public bool      Propagate              { get; set; }
    public bool      PropagateToOwner       { get; set; }
    public bool      PropagateToTenant      { get; set; }
    public string[]? PropagateRelationTypes { get; set; }
}
