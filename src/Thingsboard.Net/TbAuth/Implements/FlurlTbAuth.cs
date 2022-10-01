using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Thingsboard.Net.DependencyInjection;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbAuth;

public class FlurlTbAuth : ITbAuth
{
    private readonly ThingsboardNetOptions _options;
    private readonly ILogger<FlurlTbAuth>  _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAuth(ILogger<FlurlTbAuth>     logger,
        IOptionsSnapshot<ThingsboardNetOptions> options)
    {
        _logger  = logger;
        _options = options.Value;
    }

    public async Task<TbLoginResponse> LoginAsync(TbLoginRequest request, CancellationToken cancel = default)
    {
        var timeoutPolicy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(3,
                _ => TimeSpan.FromSeconds(1),
                (exception, span, retryCount) => _logger.LogWarning(exception, "Timeout. Retry {retryCount} in {span}", retryCount, span));

        var policy = timeoutPolicy;

        return await policy.ExecuteAsync(async () =>
        {
            var response = await _options.GetUrl()
                .AppendPathSegment("/api/auth/login")
                .AllowAnyHttpStatus()
                .WithTimeout(_options.GetTimeoutInSecOrDefault())
                .PostJsonAsync(request.ToJsonObject(), cancel);

            if (response.StatusCode >= 300)
            {
                var error = await response.GetJsonAsync<TbResponseError>();
                throw new TbHttpException(error);
            }

            return await response.GetJsonAsync<TbLoginResponse>();
        });
    }
}
