namespace Thingsboard.Net;

public class TbCountEntityDataRequest
{
    public TbEntityFilter? EntityFilter { get; set; }

    public TbKeyFilter[]? KeyFilters { get; set; }
}
