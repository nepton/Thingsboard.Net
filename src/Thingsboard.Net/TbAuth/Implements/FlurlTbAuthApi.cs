using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.TbLogin;
using Thingsboard.Net.Utility;

namespace Thingsboard.Net.TbAuth;

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
        return await _builder.CreatePolicy().ExecuteAsync(async () =>
        {
            var request  = await _builder.CreateRequest("/api/auth/user", GetCustomOptions(), cancel);
            var response = await request.GetJsonAsync<TbUserInfo>(cancel);
            return response;
        });
    }
}
