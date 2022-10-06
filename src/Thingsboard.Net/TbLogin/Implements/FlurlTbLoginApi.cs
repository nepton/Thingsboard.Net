using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Utility;

namespace Thingsboard.Net.TbLogin;

/// <summary>
/// The Thingsboard Auth API implement by flurl
/// </summary>
public class FlurlTbLoginApi : FlurlClientApi<ITbLoginApi>, ITbLoginApi
{
    private readonly IRequestBuilder _requestBuilder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbLoginApi(IRequestBuilder requestBuilder)
    {
        _requestBuilder = requestBuilder;
    }

    public async Task<TbLoginResponse> LoginAsync(TbLoginRequest loginRequest, CancellationToken cancel = default)
    {
        var policy = _requestBuilder.CreateAnonymousPolicy();

        return await policy.ExecuteAsync(async () =>
        {
            var request = _requestBuilder.CreateAnonymousRequest("api/auth/login", GetCustomOptions());
            return await request
                .PostJsonAsync(loginRequest, cancel)
                .ReceiveJson<TbLoginResponse>();
        });
    }
}
