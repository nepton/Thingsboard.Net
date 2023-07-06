namespace Thingsboard.Net;

public class TbAlarmDataQueryRequest
{
    public TbEntityField[]? AlarmFields { get; set; }

    public TbEntityField[]? EntityFields { get; set; }

    public TbEntityFilter? EntityFilter { get; set; }

    public TbKeyFilter[]? KeyFilters { get; set; }

    public TbEntityField? LatestValues { get; set; }

    public TbAlarmDataPageLink? PageLink { get; set; }
}
