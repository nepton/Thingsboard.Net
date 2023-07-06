using System;

namespace Thingsboard.Net;

/// <summary>
/// The request object that contains the parameters of the query.
/// </summary>
public class TbAlarmCountQueryRequest
{
    public TbEntityFilter? EntityFilter { get; set; }

    public TbKeyFilter[]? KeyFilters { get; set; }

    public TbEntityId? AssigneeId { get; set; }

    public DateTime? StartTs { get; set; }

    public DateTime? EndTs { get; set; }

    public bool? SearchPropagatedAlarms { get; set; }

    public TbAlarmSeverity[]? SeverityList { get; set; }

    public TbAlarmQueryStatus[]? StatusList { get; set; }

    /// <summary>
    /// The time window in milliseconds.
    /// </summary>
    public long? TimeWindow { get; set; }

    /// <summary>
    /// The alarm type.
    /// </summary>
    public string[]? TypeList { get; set; }
}
