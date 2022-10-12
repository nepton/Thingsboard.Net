using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net.Flurl.Utilities;

/// <summary>
/// Represents a policy used to save the token
/// </summary>
public interface IAccessTokenRepository
{
    /// <summary>
    /// Retrieve the token from the cache or from the server
    /// </summary>
    /// <param name="credentials"></param>
    /// <param name="accessTokenCaller"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string> GetOrAddTokenAsync(TbCredentials credentials, Func<CancellationToken, Task<TbLoginToken>> accessTokenCaller, CancellationToken cancel = default);

    /// <summary>
    /// Clear the token from the cache
    /// </summary>
    /// <returns></returns>
    Task RemoveExpiredTokenAsync(TbCredentials credentials);
}
