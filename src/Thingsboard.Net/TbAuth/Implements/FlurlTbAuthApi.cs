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

/// <summary>
/// The Thingsboard Auth API implement by flurl
/// </summary>
public class FlurlTbAuthApi : ITbAuthApi
{
    private readonly ThingsboardNetOptions   _options;
    private readonly ILogger<FlurlTbAuthApi> _logger;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAuthApi(ILogger<FlurlTbAuthApi> logger,
        IOptionsSnapshot<ThingsboardNetOptions>   options)
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

    /// <summary>
    /// Retrieves the current user.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public async Task<TbUserInfo> GetCurrentUserAsync(CancellationToken cancel = default)
    {
        // todo 缺少 Auth token
        var timeoutPolicy = Policy.Handle<FlurlHttpTimeoutException>()
            .WaitAndRetryAsync(3,
                _ => TimeSpan.FromSeconds(1),
                (exception, span, retryCount) => _logger.LogWarning(exception, "Timeout. Retry {retryCount} in {span}", retryCount, span));

        var policy = timeoutPolicy;

        return await policy.ExecuteAsync(async () =>
        {
            var result = await _options.GetUrl()
                .AppendPathSegment("/api/auth/user")
                .ConfigureRequest(action =>
                {
                    action.OnErrorAsync = async (call) =>
                    {
                        var error = await call.Response.GetJsonAsync<TbResponseError>();
                        throw new TbHttpException(error);
                    };
                })
                .WithTimeout(_options.GetTimeoutInSecOrDefault())
                .GetJsonAsync<TbUserInfo>(cancellationToken: cancel);

            return result;
        });
    }
}
