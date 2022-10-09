using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utility;
using Thingsboard.Net.Flurl.Utility.Implements;
using Thingsboard.Net.TbAuthController;

namespace Thingsboard.Net.Flurl.TbAuth;

public class FlurlTbAuthApi : FlurlClientApi<ITbAuthApi>, ITbAuthApi
{
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAuthApi(IRequestBuilder builder)
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
        var policy = _builder.GetDefaultPolicy()
            .RetryOnUnauthorized()
            .Build();

        return await policy.ExecuteAsync(async () =>
            await _builder
                .CreateRequest("/api/auth/user", GetCustomOptions())
                .GetJsonAsync<TbUserInfo>(cancel));
    }

    /// <summary>
    /// Change the password for the User which credentials are used to perform this REST API call. Be aware that previously generated JWT tokens will be still valid until they expire.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancel = default)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var policy = _builder.GetDefaultPolicy()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
            await _builder
                .CreateRequest("/api/auth/changePassword", GetCustomOptions())
                .PostJsonAsync(request, cancel));
    }
}
