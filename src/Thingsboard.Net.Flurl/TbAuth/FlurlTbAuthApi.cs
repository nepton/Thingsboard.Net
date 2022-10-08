using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utility;
using Thingsboard.Net.Flurl.Utility.Implements;
using Thingsboard.Net.TbAuth;

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
        var policy = _builder.CreatePolicy(true);

        return await policy.ExecuteAsync(async () =>
            await _builder
                .CreateRequest("/api/auth/user", GetCustomOptions(), true)
                .GetJsonAsync<TbUserInfo>(cancel));
    }
}
