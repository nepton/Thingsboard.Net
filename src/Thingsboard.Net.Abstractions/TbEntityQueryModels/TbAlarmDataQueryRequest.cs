using System;

namespace Thingsboard.Net;

public class TbAlarmDataQueryRequest
{
    /// <summary>
    /// Which fields of the alarm to fetch. If not specified, all fields are fetched.
    /// </summary>
    /// <remarks>
    /// We got 500 if we set this to null.
    /// </remarks>
    public TbEntityField[] AlarmFields { get; set; } = Array.Empty<TbEntityField>();

    public TbEntityField[]? EntityFields { get; set; }

    public TbEntityFilter? EntityFilter { get; set; }

    public TbKeyFilter[]? KeyFilters { get; set; }

    public TbEntityField? LatestValues { get; set; }

    public TbAlarmDataPageLink? PageLink { get; set; }
}
