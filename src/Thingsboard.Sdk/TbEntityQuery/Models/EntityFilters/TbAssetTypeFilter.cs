namespace Thingsboard.Sdk.TbEntityQuery;

/// <summary>
/// Allows to filter assets based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'charging station' assets which name starts with 'Tesla':
/// </summary>
public class TbAssetTypeFilter : TbEntityFilter
{
    public string AssetType { get; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string AssetNameStartsWith { get; }

    public TbAssetTypeFilter(string assetType, string assetNameStartsWith)
    {
        AssetType           = assetType;
        AssetNameStartsWith = assetNameStartsWith;
    }

    public override object ToQuery()
    {
        return new
        {
            type            = "assetType",
            assetType       = AssetType,
            assetNameFilter = AssetNameStartsWith,
        };
    }
}
