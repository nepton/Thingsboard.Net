using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Common;

namespace Thingsboard.Net.TbLogin;

/// <summary>
/// Thingsboard login endpoint
/// </summary>
public interface ITbLoginApi : IClientApi<ITbLoginApi>
{
    /// <summary>
    /// Login method used to authenticate user and get JWT token data.
    /// Value of the response token field can be used as X-Authorization header value:
    /// X-Authorization: Bearer $JWT_TOKEN_VALUE.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbLoginResponse> LoginAsync(TbLoginRequest request, CancellationToken cancel = default);
}
