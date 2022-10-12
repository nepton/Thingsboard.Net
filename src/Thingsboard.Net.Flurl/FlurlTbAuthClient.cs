using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbAuthClient : FlurlTbClient<ITbAuthClient>, ITbAuthClient
{
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAuthClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Retrieves the current user.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TbUserInfo> GetCurrentUserAsync(CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return await policy.ExecuteAsync(async () =>
            await builder.CreateRequest()
                .AppendPathSegment("/api/auth/user")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbUserInfo>(cancel));
    }

    /// <summary>
    /// Change the password for the User which credentials are used to perform this REST API call. Be aware that previously generated JWT tokens will be still valid until they expire.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task ChangePasswordAsync(TbChangePasswordRequest request, CancellationToken cancel = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
            await builder.CreateRequest()
                .AppendPathSegment("/api/auth/changePassword")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(request, cancel));
    }
}
