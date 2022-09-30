using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Polly;
using Thingsboard.Sdk.Utility;

namespace Thingsboard.Sdk.TbAuth;

public class FlurlTbAuth : ITbAuth
{
    private readonly IRequestBuilder _requestBuilder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAuth(IRequestBuilder requestBuilder)
    {
        _requestBuilder = requestBuilder;
    }

    public async Task<TbLoginResponse> LoginAsync(TbLoginRequest request, CancellationToken cancel = default)
    {
        var policy = Policy.Handle<FlurlHttpException>(x => x.StatusCode >= 500)
            .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(1));

        return await policy.ExecuteAsync(async () =>
        {
            var login = _requestBuilder.CreateRequest("/api/auth/login");
            return await login.PostJsonAsync(request, cancel).ReceiveJson<TbLoginResponse>();
        });
    }
}
