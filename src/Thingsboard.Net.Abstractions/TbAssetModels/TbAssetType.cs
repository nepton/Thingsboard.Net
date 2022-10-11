namespace Thingsboard.Net;

public class TbAssetType
{
    public TbEntityId?  TenantId   { get; set; }
    public TbEntityType EntityType { get; set; }
    public string?      Type       { get; set; }
}