namespace Thingsboard.Net
{
    /// <summary>
    /// EntityDataPageLink
    /// </summary>
    public class TbEntityDataPageLink
    {
        public bool? Dynamic { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public TbEntityQuerySortOrder? SortOrder { get; set; }

        public string? TextSearch { get; set; }
    }
}
