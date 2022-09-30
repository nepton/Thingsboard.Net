using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace Thingsboard.Sdk.Tokens;

/// <summary>
/// Represents a policy used to save the token
/// </summary>
public interface IAccessTokenCaching
{
    IAsyncPolicy GetTokenExpiredPolicy(string username);

    Task<string> GetAccessTokenAsync(string username, string password, CancellationToken cancel);
}
