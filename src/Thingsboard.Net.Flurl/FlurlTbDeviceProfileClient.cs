using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// Thingsboard device controller implements by flurl
/// </summary>
public class FlurlTbDeviceProfileClient : FlurlTbClient<ITbDeviceProfileClient>, ITbDeviceProfileClient
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbDeviceProfileClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Create or update the Device Profile. When creating device profile, platform generates device profile id as time-based UUID. The newly created device profile id will be present in the response. Specify existing device profile id to update the device profile. Referencing non-existing device profile Id will cause 'Not Found' error.
    /// Device profile name is unique in the scope of tenant.Only one 'default' device profile may exist in scope of tenant.
    /// </summary>
    /// <param name="deviceProfile">New device profile</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// DeviceProfile name is unique in the scope of tenant. Use unique identifiers like MAC or IMEI for the device names and non-unique 'label' field for user-friendly visualization purposes.Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new DeviceProfile entity.
    /// </remarks>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    public Task<TbDeviceProfile> SaveDeviceProfileAsync(TbNewDeviceProfile deviceProfile, CancellationToken cancel = default)
    {
        if (deviceProfile == null) throw new ArgumentNullException(nameof(deviceProfile));

        var policy = RequestBuilder.GetPolicyBuilder<TbDeviceProfile>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/deviceProfile")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(deviceProfile, cancel)
                .ReceiveJson<TbDeviceProfile>();

            return response;
        });
    }

    /// <summary>
    /// Create or update the Device Profile. When creating device profile, platform generates device profile id as time-based UUID. The newly created device profile id will be present in the response. Specify existing device profile id to update the device profile. Referencing non-existing device profile Id will cause 'Not Found' error.
    /// Device profile name is unique in the scope of tenant.Only one 'default' device profile may exist in scope of tenant.
    /// </summary>
    /// <param name="deviceProfile">New device info</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    public Task<TbDeviceProfile> SaveDeviceProfileAsync(TbDeviceProfile deviceProfile, CancellationToken cancel = default)
    {
        if (deviceProfile == null) throw new ArgumentNullException(nameof(deviceProfile));

        var policy = RequestBuilder.GetPolicyBuilder<TbDeviceProfile>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(deviceProfile.Id))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/deviceProfile")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(deviceProfile, cancel)
                .ReceiveJson<TbDeviceProfile>();

            return response;
        });
    }

    /// <summary>
    /// Fetch the Device Profile object based on the provided Device Profile Id. The server checks that the device profile is owned by the same tenant.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the device object, or null if it does not exist</returns>
    public Task<TbDeviceProfile?> GetDeviceProfileByIdAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbDeviceProfile?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfile/{deviceProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbDeviceProfile?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes the device profile. Referencing non-existing device profile Id will cause an error. Can't delete the device profile if it is referenced by existing devices.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteDeviceProfileAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.DEVICE_PROFILE, deviceProfileId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfile/{deviceProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Marks device profile as default within a tenant scope.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task SetDefaultDeviceProfileAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.DEVICE_PROFILE, deviceProfileId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfile/{deviceProfileId}/default")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostAsync(null, cancel);
        });
    }

    /// <summary>
    /// Get a set of unique attribute keys used by devices that belong to specified profile. If profile is not set returns a list of unique keys among all profiles. The call is used for auto-complete in the UI forms. The implementation limits the number of devices that participate in search to 100 as a trade of between accurate results and time-consuming queries.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<string[]> GetAttributeKeysAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<string[]>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.DEVICE_PROFILE, deviceProfileId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfile/devices/keys/attributes")
                .SetQueryParam("deviceProfileId", deviceProfileId)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<string[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Get a set of unique time-series keys used by devices that belong to specified profile. If profile is not set returns a list of unique keys among all profiles. The call is used for auto-complete in the UI forms. The implementation limits the number of devices that participate in search to 100 as a trade of between accurate results and time-consuming queries.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<string[]> GetTimeSeriesKeysAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<string[]>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.DEVICE_PROFILE, deviceProfileId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfile/devices/keys/timeseries")
                .SetQueryParam("deviceProfileId", deviceProfileId)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<string[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Fetch the DeviceProfile Info object based on the provided DeviceProfile Id. If the user has the authority of 'Tenant Administrator', the server checks that the device is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the device is assigned to the same customer. DeviceProfile Info is an extension of the default DeviceProfile object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDeviceProfileInfo?> GetDeviceProfileInfoByIdAsync(Guid deviceProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbDeviceProfileInfo?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfileInfo/{deviceProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbDeviceProfileInfo?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Fetch the Default Device Profile Info object. Device Profile Info is a lightweight object that includes main information about Device Profile excluding the heavyweight configuration object.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDeviceProfileInfo?> GetDefaultDeviceProfileInfoAsync(CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbDeviceProfileInfo?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfileInfo/default")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbDeviceProfileInfo?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a page of devices profile info objects owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details. Device Profile Info is a lightweight object that includes main information about Device Profile excluding the heavyweight configuration object.
    /// </summary>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="transportType">Type of the transport</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDeviceProfileInfo>> GetDeviceProfileInfosAsync(
        int                                pageSize,
        int                                page,
        string?                            textSearch    = null,
        TbDeviceProfileSearchSortProperty? sortProperty  = null,
        TbSortOrder?                       sortOrder     = null,
        TbDeviceProfileTransportType?      transportType = null,
        CancellationToken                  cancel        = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbDeviceProfileInfo>>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbDeviceProfileInfo>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfileInfos")
                .SetQueryParam("pageSize",      pageSize)
                .SetQueryParam("page",          page)
                .SetQueryParam("textSearch",    textSearch)
                .SetQueryParam("sortProperty",  sortProperty)
                .SetQueryParam("sortOrder",     sortOrder)
                .SetQueryParam("transportType", transportType)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbPage<TbDeviceProfileInfo>>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Obtaining the DeviceProfile List
    /// </summary>
    /// <param name="type">Type of device</param>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDeviceProfile>> GetDeviceProfilesAsync(
        int                                pageSize,
        int                                page,
        string?                            textSearch   = null,
        TbDeviceProfileSearchSortProperty? sortProperty = null,
        TbSortOrder?                       sortOrder    = null,
        CancellationToken                  cancel       = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbDeviceProfile>>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbDeviceProfile>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/deviceProfiles")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbPage<TbDeviceProfile>>(cancel);

            return response;
        });
    }
}
