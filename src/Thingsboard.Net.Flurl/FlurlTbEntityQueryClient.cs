using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

public class FlurlTbEntityQueryClient : FlurlTbClient<ITbEntityQueryClient>, ITbEntityQueryClient
{
    public FlurlTbEntityQueryClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Allows to run complex queries over platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the paginated result of the query that contains requested entity fields and latest values of requested attributes and time-series data.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbFindEntityDataResponse>> FindEntityDataByQueryAsync(TbFindEntityDataRequest request, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbFindEntityDataResponse>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbFindEntityDataResponse>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/entitiesQuery/find")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(request, cancel)
                .ReceiveJson<TbPage<TbFindEntityDataResponse>>();

            return response;
        });
    }

    /// <summary>
    /// Allows to run complex queries to search the count of platform entities (devices, assets, customers, etc) based on the combination of main entity filter and multiple key filters. Returns the number of entities that match the query definition.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<int> CountEntityDataByQueryAsync(TbCountEntityDataRequest request, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<int>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, 0)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/entitiesQuery/count")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(request, cancel)
                .ReceiveJson<int>();

            return response;
        });
    }
}
