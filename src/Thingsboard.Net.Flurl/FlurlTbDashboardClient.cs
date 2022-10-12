using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbDashboardClient : FlurlTbClient<ITbDashboardClient>, ITbDashboardClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbDashboardClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Get the server time (milliseconds since January 1, 1970 UTC). Used to adjust view of the dashboards according to the difference between browser and server time.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<DateTime> GetServerTimeAsync(CancellationToken cancel = default)
    {
        var builder = _builder.MergeCustomOptions(CustomOptions);

        var policy = builder.GetPolicyBuilder<DateTime>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
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
