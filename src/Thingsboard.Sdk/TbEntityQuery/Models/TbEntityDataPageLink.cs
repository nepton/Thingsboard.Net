namespace Thingsboard.Sdk.TbEntityQuery
{
    /// <summary>
    /// EntityDataPageLink
    /// </summary>
    public class TbEntityDataPageLink
    {
        public bool? Dynamic { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public TbSortOrder? SortOrder { get; set; }

        public string? TextSearch { get; set; }

        public object ToQuery()
        {
            return new
            {
                dynamic    = Dynamic,
                page       = Page,
                pageSize   = PageSize,
                sortOrder  = SortOrder?.ToQuery(),
                textSearch = TextSearch
            };
        }
    }
}
