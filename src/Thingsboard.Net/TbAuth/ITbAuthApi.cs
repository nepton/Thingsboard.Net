using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net.TbAuth;

/// <summary>
/// Thingsboard login endpoint
/// </summary>
public interface ITbAuthApi
{
    Task<TbLoginResponse> LoginAsync(TbLoginRequest request, CancellationToken cancel = default);
}