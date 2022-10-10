using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net.TbAttributes;

/// <summary>
/// Attribute 读写操作
/// </summary>
public interface ITbAttributeService
{
    /// <summary>
    /// 获取指定Key的属性
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="entityId">实体Id</param>
    /// <param name="keys">要获取的 Key 数组</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityValue[]?> GetAttributesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbEntityField[]     keys,
        CancellationToken cancel = default);

    /// <summary>
    /// 保存设备 Attributes
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="entityId">实体Id</param>
    /// <param name="attributes">属性值</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SaveAttributesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        TbEntityValue[]   attributes,
        CancellationToken cancel = default);
}
