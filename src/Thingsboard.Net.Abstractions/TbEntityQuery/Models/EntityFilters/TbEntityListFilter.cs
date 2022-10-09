using System;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbEntityQuery;

public class TbEntityListFilter : TbEntityFilter
{
    public override string Type => "entityList";

    public TbEntityListFilter()
    {
    }

    public TbEntityListFilter(TbEntityType type, Guid[] entityList)
    {
        EntityList = entityList;
        EntityType = type;
    }

    public Guid[]? EntityList { get; }

    public TbEntityType EntityType { get; set; }
}
