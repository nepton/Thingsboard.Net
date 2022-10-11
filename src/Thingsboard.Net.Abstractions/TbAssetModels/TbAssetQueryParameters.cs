namespace Thingsboard.Net;

public class TbAssetQueryParameters
{
    public string? RootId             { get; set; }
    public string? RootType           { get; set; }
    public string? Direction          { get; set; }
    public string? RelationTypeGroup  { get; set; }
    public int     MaxLevel           { get; set; }
    public bool    FetchLastLevelOnly { get; set; }
}