using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbSortOrder
{
    public TbEntityField?       Key       { get; set; }
    public TbSortOrderDirection Direction { get; set; }

    public object ToQuery()
    {
        return new
        {
            key       = Key,
            direction = Direction
        };
    }
}
