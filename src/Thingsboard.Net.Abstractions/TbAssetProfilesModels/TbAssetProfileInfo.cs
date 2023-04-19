namespace Thingsboard.Net;

public class TbAssetProfileInfo
{
    public TbAssetProfileInfo(TbEntityId id)
    {
        Id = id;
    }

    public TbEntityId Id { get; }
    public string? Name { get; set; }
    public object? Image { get; set; }
    public object? DefaultDashboardId { get; set; }
    public string? Type { get; set; }
}
