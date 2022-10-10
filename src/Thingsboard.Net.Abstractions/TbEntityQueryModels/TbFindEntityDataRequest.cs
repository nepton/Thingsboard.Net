namespace Thingsboard.Net;

public class TbFindEntityDataRequest
{
    public TbEntityFilter?       EntityFilter { get; set; }
    public TbKeyFilter?          KeyFilter    { get; set; }
    public TbEntityField[]?      EntityFields { get; set; }
    public TbEntityField[]?      LatestValues { get; set; }
    public TbEntityDataPageLink? PageLink     { get; set; }
}
