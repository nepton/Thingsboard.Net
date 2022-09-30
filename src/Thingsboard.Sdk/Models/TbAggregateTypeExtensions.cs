using System;

namespace Thingsboard.Sdk.Models;

public static class TbAggregateTypeExtensions
{
    public static string ToTbName(this TbAggregateType source)
    {
        return source switch
        {
            TbAggregateType.Sum   => "SUM",
            TbAggregateType.Min   => "MIN",
            TbAggregateType.Max   => "MAX",
            TbAggregateType.Avg   => "AVG",
            TbAggregateType.Count => "COUNT",
            _                     => throw new NotSupportedException(),
        };
    }
}
