using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// The Thingsboard Auth API implement by flurl
/// </summary>
public class FlurlTbLoginClient : FlurlClientApi<ITbLoginClient>, ITbLoginClient
{
    private readonly IRequestBuilder _requestBuilder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbLoginClient(IRequestBuilder requestBuilder)
    {
        _requestBuilder = requestBuilder;
    }

    public async Task<TbLoginResponse> LoginAsync(TbLoginRequest loginRequest, CancellationToken cancel = default)
    {
        var policy = _requestBuilder.GetDefaultPolicy().Build();

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
