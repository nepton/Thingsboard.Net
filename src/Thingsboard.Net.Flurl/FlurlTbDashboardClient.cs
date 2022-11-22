using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

public class FlurlTbDashboardClient : FlurlTbClient<ITbDashboardClient>, ITbDashboardClient
{
    public FlurlTbDashboardClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Get the server time (milliseconds since January 1, 1970 UTC). Used to adjust view of the dashboards according to the difference between browser and server time.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<DateTime> GetServerTimeAsync(CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<DateTime>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/dashboard/serverTime")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetStringAsync(cancel);

            if (!long.TryParse(response, out var result))
                throw new TbException("Can't parse server time");

            return result.ToDateTime();
        });
    }
}
