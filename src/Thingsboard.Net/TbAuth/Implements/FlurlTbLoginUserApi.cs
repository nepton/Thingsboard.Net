using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Utility;

namespace Thingsboard.Net.TbAuth;

public class FlurlTbLoginUserApi : FlurlClientApi<ITbLoginUserApi>, ITbLoginUserApi
{
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbLoginUserApi(IRequestBuilder builder)
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
            var request  = await _builder.CreateRequest("/api/auth/user", GetOptions(), cancel);
            var response = await request.GetJsonAsync<TbUserInfo>(cancellationToken: cancel);
            return response;
        });
    }
}
