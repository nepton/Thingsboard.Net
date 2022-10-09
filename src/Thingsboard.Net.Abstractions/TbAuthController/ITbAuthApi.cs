using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Common;

namespace Thingsboard.Net.TbAuthController;

public interface ITbAuthApi : IClientApi<ITbAuthApi>
{
    /// <summary>
    /// Retrieves the current user.
    /// /api/auth/user
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbUserInfo> GetCurrentUserAsync(CancellationToken cancel = default);

    /// <summary>
    /// Change the password for the User which credentials are used to perform this REST API call. Be aware that previously generated JWT tokens will be still valid until they expire.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancel = default);
}
