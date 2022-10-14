using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// The Thingsboard Auth API implement by flurl
/// </summary>
public class FlurlTbLoginClient : FlurlTbClient<ITbLoginClient>, ITbLoginClient
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbLoginClient(IRequestBuilder builder) : base(builder)
    {
    }

    public Task<TbLoginToken> LoginAsync(TbLoginUser loginRequest, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbLoginToken>()
            .RetryOnHttpTimeout()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/auth/login")
                .PostJsonAsync(loginRequest, cancel)
                .ReceiveJson<TbLoginToken>();

            return response;
        });
    }
}
