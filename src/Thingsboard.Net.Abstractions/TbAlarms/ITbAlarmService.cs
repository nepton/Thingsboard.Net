using System;
using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbAlarms;

/// <summary>
/// 警报服务
/// </summary>
public interface ITbAlarmService
{
    /// <summary>
    /// 确认指定Id的报警信息
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task AcknowledgeAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default);

    /// <summary>
    /// 清除指定Id的报警信息
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task ClearAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default);

    /// <summary>
    /// 获取指定Id的报警信息
    /// </summary>
    /// <param name="tbAlarmId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAlarm?> GetAlarmAsync(Guid tbAlarmId, CancellationToken cancel = default);

    /// <summary>
    /// 获取报警信息
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<PageCollection<TbAlarm>> GetAlarmsAsync(TbAlarmQueryFilter filter, CancellationToken cancel = default);
}