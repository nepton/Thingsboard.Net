using System;
using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Sdk.Models;

namespace Thingsboard.Sdk.TbTimeSeries;

public interface ITbTimeSeriesService
{
    /// <summary>
    /// 获取指定Id实体的TimeSeries值
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="entityId">实体Id</param>
    /// <param name="keys">要获取的 Key 数组</param>
    /// <param name="useStrictDataTypes"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityValue[]> GetLatestTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        bool?             useStrictDataTypes = null,
        CancellationToken cancel             = default);

    /// <summary>
    /// 获取指定Id实体的TimeSeries值
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="entityId">实体Id</param>
    /// <param name="keys">要获取的 Key 数组</param>
    /// <param name="start"></param>
    /// <param name="end">结束时间</param>
    /// <param name="aggregateType">聚类方式</param>
    /// <param name="intervalInMs">记录的聚合间隔, 单位为毫秒</param>
    /// <param name="sortOrder"></param>
    /// <param name="useStrictDataTypes"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbTimeSeries[]> GetAggregatedTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        DateTime          start,
        DateTime          end,
        TbAggregateType   aggregateType,
        long?             intervalInMs,
        TbSortOrderDirection?      sortOrder          = null,
        bool?             useStrictDataTypes = null,
        CancellationToken cancel             = default);

    /// <summary>
    /// 获取指定Id实体的TimeSeries值
    /// </summary>
    /// <param name="entityType">实体类型</param>
    /// <param name="entityId">实体Id</param>
    /// <param name="keys">要获取的 Key 数组</param>
    /// <param name="start"></param>
    /// <param name="end">结束时间</param>
    /// <param name="limit">限制获取的最大记录数</param>
    /// <param name="sortOrder"></param>
    /// <param name="useStrictDataTypes"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbTimeSeries[]> GetTimeSeriesAsync(
        TbEntityType      entityType,
        Guid              entityId,
        string[]          keys,
        DateTime          start,
        DateTime          end,
        int?              limit              = null,
        TbSortOrderDirection?      sortOrder          = null,
        bool?             useStrictDataTypes = null,
        CancellationToken cancel             = default);
}
