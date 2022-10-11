using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
/// Thingsboard REST API client dashboard controller.
/// </summary>
public interface ITbDashboardClient : ITbClient<ITbDashboardClient>
{
    /// <summary>
    /// Get the server time (milliseconds since January 1, 1970 UTC). Used to adjust view of the dashboards according to the difference between browser and server time.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<DateTime> GetServerTimeAsync(CancellationToken cancel=default);
}