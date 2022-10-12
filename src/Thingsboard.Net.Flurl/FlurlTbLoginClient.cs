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
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbLoginClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    public async Task<TbLoginToken> LoginAsync(TbLoginUser loginRequest, CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<TbLoginToken>()
            .RetryOnHttpTimeout()
            .Build();

        return await policy.ExecuteAsync(async () =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/auth/login")
                .PostJsonAsync(loginRequest, cancel)
                .ReceiveJson<TbLoginToken>();

            return response;
        });
    }
}
