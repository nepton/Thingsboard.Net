using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utility;
using Thingsboard.Net.Flurl.Utility.Implements;
using Thingsboard.Net.TbLogin;

namespace Thingsboard.Net.Flurl.TbLogin;

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
        var policy = _requestBuilder.CreatePolicy(false);

        return await policy.ExecuteAsync(async () =>
        {
            var response = await _requestBuilder
                .CreateRequest("api/auth/login", GetCustomOptions(), false)
                .PostJsonAsync(loginRequest, cancel)
                .ReceiveJson<TbLoginResponse>();

            return response;
        });
    }
}
