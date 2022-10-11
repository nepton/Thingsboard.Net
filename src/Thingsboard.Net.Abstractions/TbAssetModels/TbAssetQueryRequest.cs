namespace Thingsboard.Net;

public class TbAssetQueryRequest
{
    public string?                 RelationType { get; set; }
    public string[]?               AssetTypes   { get; set; }
    public TbAssetQueryParameters? Parameters   { get; set; }
}