namespace Thingsboard.Net.Models;

/// <summary>
/// Pageable query
/// </summary>
public class PageableQueryFilter
{
    /// <summary>
    /// 查询前需要跳过的记录数 (默认 0)
    /// </summary>
    public int Skip { get; set; } = 0;

    /// <summary>
    /// 本次查询返回的记录数 (默认 20)
    /// </summary>
    public int Take { get; set; } = 20;
}