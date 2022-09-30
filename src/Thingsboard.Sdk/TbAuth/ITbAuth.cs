using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Sdk.TbAuth;

/// <summary>
/// Thingsboard login endpoint
/// </summary>
public interface ITbAuth
{
    Task<TbLoginResponse> LoginAsync(TbLoginRequest request, CancellationToken cancel = default);
}