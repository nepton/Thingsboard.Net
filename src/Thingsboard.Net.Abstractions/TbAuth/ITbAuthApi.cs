using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Common;

namespace Thingsboard.Net.TbAuth;

public interface ITbAuthApi : IClientApi<ITbAuthApi>
{
    /// <summary>
    /// Retrieves the current user.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbUserInfo> GetCurrentUserAsync(CancellationToken cancel = default);
}
