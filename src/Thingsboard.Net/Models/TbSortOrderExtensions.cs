using System;

namespace Thingsboard.Net.Models;

public static class TbSortOrderExtensions
{
    /// <summary>
    /// 获取 tb 的排序的方式名
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static string ToTbName(this TbSortOrderDirection source)
    {
        return source switch
        {
            TbSortOrderDirection.Asc  => "ASC",
            TbSortOrderDirection.Desc => "DESC",
            _                => throw new NotSupportedException(),
        };
    }
}
