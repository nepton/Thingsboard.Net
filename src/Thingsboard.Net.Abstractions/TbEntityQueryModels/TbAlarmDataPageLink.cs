using System;

namespace Thingsboard.Net;

/// <summary>
/// Alarm query page link
/// </summary>
public class TbAlarmDataPageLink
{
    public TbEntityId? AssigneeId { get; set; }

    public bool Dynamic { get; set; }

    public DateTime? EndTs { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public bool? SearchPropagatedAlarms { get; set; }

    public TbAlarmSeverity[]? SeverityList { get; set; }

    public TbEntityQuerySortOrder? SortOrder { get; set; }

    public DateTime? StartTs { get; set; }

    public TbAlarmQueryStatus[]? StatusList { get; set; }

    public string? TextSearch { get; set; }

    public long? TimeWindow { get; set; }

    public string[]? TypeList { get; set; }
}
