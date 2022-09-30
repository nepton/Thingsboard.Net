using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbDevices;

/// <summary>
/// 设备从 Thingsboard 查询的接口
/// </summary>
public interface ITbDeviceService
{
    /// <summary>
    /// 获取设备列表
    /// </summary>
    /// <param name="type">设备的类型</param>
    /// <param name="textSearch">搜索条件</param>
    /// <param name="sortProperty">排序属性</param>
    /// <param name="sortOrder">排序方向</param>
    /// <param name="take">读取的记录数</param>
    /// <param name="cancel"></param>
    /// <param name="skip">读取前跳过的记录数</param>
    /// <returns></returns>
    Task<PageCollection<TbDevice>> GetTenantDevicesAsync(
        string?           type         = null,
        string?           textSearch   = null,
        string?           sortProperty = null,
        TbSortOrderDirection?      sortOrder    = null,
        int               skip         = 0,
        int               take         = 20,
        CancellationToken cancel       = default);

    /// <summary>
    /// 通过Id获取指定的设备, 如果不存在, 返回 null
    /// </summary>
    /// <param name="deviceId">设备Id</param>
    /// <param name="cancel"></param>
    /// <returns>返回设备对象, 如果不存在, 返回 null</returns>
    Task<TbDevice?> GetDeviceByIdAsync(
        string            deviceId,
        CancellationToken cancel = default);
}
