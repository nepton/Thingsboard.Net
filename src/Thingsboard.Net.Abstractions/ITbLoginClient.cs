using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
/// Thingsboard login endpoint
/// </summary>
public interface ITbLoginClient : ITbClient<ITbLoginClient>
{
    /// <summary>
    /// Login method used to authenticate user and get JWT token data.
    /// Value of the response token field can be used as X-Authorization header value:
    /// X-Authorization: Bearer $JWT_TOKEN_VALUE.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbLoginToken> LoginAsync(TbLoginUser request, CancellationToken cancel = default);
}
