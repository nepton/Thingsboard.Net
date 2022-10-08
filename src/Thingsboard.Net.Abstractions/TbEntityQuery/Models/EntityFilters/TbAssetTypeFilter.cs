namespace Thingsboard.Net.TbEntityQuery;

/// <summary>
/// Allows to filter assets based on their type and the 'starts with' expression over their name.
/// For example, this entity filter selects all 'charging station' assets which name starts with 'Tesla':
/// </summary>
public class TbAssetTypeFilter : TbEntityFilter
{
    /// <summary>
    /// The type of filter
    /// </summary>
    public override string Type => "assetType";

    /// <summary>
    /// The asset type
    /// </summary>
    public string? AssetType { get; set; }

    /// <summary>
    /// 'starts with' expression over their name
    /// </summary>
    public string? AssetNameFilter { get; set; }

    public TbAssetTypeFilter()
    {
    }

    public TbAssetTypeFilter(string assetType, string assetNameFilter)
    {
        AssetType       = assetType;
        AssetNameFilter = assetNameFilter;
    }
}
