using System;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbAlarms;

public class TbAlarmQueryFilter : PageableQueryFilter
{
    public TbEntityId? EntityId { get; set; }

    public bool? Acknowledged { get; set; }

    public bool? Cleared { get; set; }

    public string? TextSearch { get; set; }

    public DateTime? CreatedTimeFrom { get; set; }

    public DateTime?    CreatedTimeTo { get; set; }
    public TbSortOrderDirection? SortOrder     { get; set; }
    public string?      SortProperty  { get; set; }
}
