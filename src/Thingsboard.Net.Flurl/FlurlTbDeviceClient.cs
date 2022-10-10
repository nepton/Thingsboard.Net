using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// Thingsboard device controller implements by flurl
/// </summary>
public class FlurlTbDeviceClient : FlurlTbClient<ITbDeviceClient>, ITbDeviceClient
{
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbDeviceClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Creates assignment of the device to customer. Customer will be able to query device afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDevice?> AssignDeviceToCustomerAsync(Guid customerId, Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice?>().RetryOnUnauthorized().Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"api/customer/{customerId}/device/{deviceId}")
                .PostJsonAsync(null, cancel)
                .ReceiveJson<TbDevice>();

            return response;
        });
    }

    /// <summary>
    /// Returns a page of devices info objects assigned to customer. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details. Device Info is an extension of the default Device object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Device type as the name of the device profile</param>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the device name.</param>
    /// <param name="sortProperty">Property of entity to sort by</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDevice>> GetCustomerDeviceInfosAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type,
        Guid?                      deviceProfileId,
        string?                    textSearch,
        TbDeviceSearchSortProperty sortProperty,
        TbSortOrderDirection       sortOrder,
        CancellationToken          cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbPage<TbDevice>>().RetryOnUnauthorized().Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"api/customer/{customerId}/deviceInfos")
                .SetQueryParam("pageSize",        pageSize)
                .SetQueryParam("page",            page)
                .SetQueryParam("type",            type)
                .SetQueryParam("deviceProfileId", deviceProfileId)
                .SetQueryParam("textSearch",      textSearch)
                .SetQueryParam("sortProperty",    sortProperty)
                .SetQueryParam("sortOrder",       sortOrder)
                .GetJsonAsync<TbPage<TbDevice>>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a page of devices objects assigned to customer. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Device type as the name of the device profile</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the device name.</param>
    /// <param name="sortProperty">Property of entity to sort by</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDevice>> GetCustomerDevicesAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type,
        string?                    textSearch,
        TbDeviceSearchSortProperty sortProperty,
        TbSortOrderDirection       sortOrder,
        CancellationToken          cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbPage<TbDevice>>().RetryOnUnauthorized().Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"api/customer/{customerId}/devices")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .GetJsonAsync<TbPage<TbDevice>>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Clears assignment of the device to customer. Customer will not be able to query device afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDevice?> UnassignDeviceFromCustomerAsync(Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice?>().RetryOnUnauthorized().Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"api/customer/device/{deviceId}")
                .DeleteAsync(cancel)
                .ReceiveJson<TbDevice>();

            return response;
        });
    }

    /// <summary>
    /// Create or update the Device. When creating device, platform generates Device Id as time-based UUID. Device credentials are also generated if not provided in the 'accessToken' request parameter. The newly created device id will be present in the response. Specify existing Device id to update the device. Referencing non-existing device Id will cause 'Not Found' error.
    /// </summary>
    /// <param name="device">New device info</param>
    /// <param name="accessToken">Optional value of the device credentials to be used during device creation. If omitted, access token will be auto-generated.</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// Device name is unique in the scope of tenant. Use unique identifiers like MAC or IMEI for the device names and non-unique 'label' field for user-friendly visualization purposes.Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new Device entity.
    /// </remarks>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    public Task<TbDevice> SaveDeviceAsync(TbDevice device, string? accessToken, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("api/device")
                .SetQueryParam("accessToken", accessToken)
                .PostJsonAsync(device, cancel)
                .ReceiveJson<TbDevice>();

            return response;
        });
    }

    /// <summary>
    /// Obtaining the Device List
    /// </summary>
    /// <param name="type">Type of device</param>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDevice>> GetTenantDevicesAsync(
        string?               type         = null,
        string?               textSearch   = null,
        string?               sortProperty = null,
        TbSortOrderDirection? sortOrder    = null,
        int                   page         = 0,
        int                   pageSize     = 20,
        CancellationToken     cancel       = default)
    {
        var policy = _builder.GetDefaultPolicy<TbPage<TbDevice>>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var result = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("api/tenant/devices")
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .SetQueryParam("page",         page)
                .SetQueryParam("pageSize",     pageSize)
                .GetJsonAsync<TbPage<TbDevice>>(cancel);

            return result;
        });
    }

    /// <summary>
    /// Gets the specified device by Id. If the device does not exist, null is returned
    /// </summary>
    /// <param name="deviceId">device Id</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the device object, or null if it does not exist</returns>
    public Task<TbDevice?> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice?>()
            .FallbackOnNotFound(null)
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/device/{deviceId}")
                .GetJsonAsync<TbDevice>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes the device, it's credentials and all the relations (from and to the device). Referencing non-existing device Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteDeviceAsync(Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"api/device/{deviceId}")
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Fetch the Device Info object based on the provided Device Id. If the user has the authority of 'Tenant Administrator', the server checks that the device is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the device is assigned to the same customer. Device Info is an extension of the default Device object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDeviceInfo?> GetDeviceInfoByIdAsync(Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDeviceInfo?>()
            .RetryOnUnauthorized()
            .FallbackOnNotFound(null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/device/info/{deviceId}")
                .GetJsonAsync<TbDeviceInfo>(cancel);

            return response;
        });
    }

    /// <summary>
    /// If during device creation there wasn't specified any credentials, platform generates random 'ACCESS_TOKEN' credentials.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDeviceCredential?> GetDeviceCredentialsAsync(Guid deviceId, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDeviceCredential?>()
            .RetryOnUnauthorized()
            .FallbackOnNotFound(null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/device/{deviceId}/credentials")
                .GetJsonAsync<TbDeviceCredential>(cancel);

            return response;
        });
    }

    /// <summary>
    /// During device creation, platform generates random 'ACCESS_TOKEN' credentials. Use this method to update the device credentials. First use 'getDeviceCredentialsByDeviceId' to get the credentials id and value. Then use current method to update the credentials type and value. It is not possible to create multiple device credentials for the same device. The structure of device credentials id and value is simple for the 'ACCESS_TOKEN' but is much more complex for the 'MQTT_BASIC' or 'LWM2M_CREDENTIALS'.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="credential"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDeviceCredential> UpdateDeviceCredentialsAsync(TbDeviceCredential credential, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDeviceCredential>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/device/{credential.DeviceId}/credentials")
                .PostJsonAsync(credential, cancel)
                .ReceiveJson<TbDeviceCredential>();

            return response;
        });
    }

    /// <summary>
    /// Requested devices must be owned by tenant or assigned to customer which user is performing the request.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceIds">A list of devices ids</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDevice[]> GetDevicesByIds(Guid[] deviceIds, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice[]>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/devices")
                .SetQueryParam("deviceIds", string.Join(",", deviceIds.Select(x => x.ToString())))
                .GetJsonAsync<TbDevice[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Obtaining the Device List
    /// </summary>
    /// <param name="type">Type of device</param>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDeviceInfo>> GetTenantDeviceInfosAsync(
        int                         pageSize,
        int                         page,
        string?                     type            = null,
        Guid?                       deviceProfileId = null,
        string?                     textSearch      = null,
        TbDeviceSearchSortProperty? sortProperty    = null,
        TbSortOrderDirection?       sortOrder       = null,
        CancellationToken           cancel          = default)
    {
        var policy = _builder.GetDefaultPolicy<TbPage<TbDeviceInfo>>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("/api/tenant/deviceInfos")
                .SetQueryParam("pageSize",        pageSize)
                .SetQueryParam("page",            page)
                .SetQueryParam("type",            type)
                .SetQueryParam("deviceProfileId", deviceProfileId)
                .SetQueryParam("textSearch",      textSearch)
                .SetQueryParam("sortProperty",    sortProperty)
                .SetQueryParam("sortOrder",       sortOrder)
                .GetJsonAsync<TbPage<TbDeviceInfo>>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Requested device must be owned by tenant that the user belongs to. Device name is an unique property of device. So it can be used to identify the device.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceName">A string value representing the Device name.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbDevice?> GetTenantDeviceByNameAsync(string deviceName, CancellationToken cancel = default)
    {
        var policy = _builder.GetDefaultPolicy<TbDevice?>()
            .RetryOnUnauthorized()
            .FallbackOnNotFound(null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("/api/tenant/devices")
                .SetQueryParam("deviceName", deviceName)
                .GetJsonAsync<TbDevice>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a page of devices owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="type">Type of device</param>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbDevice>> GetTenantDevicesAsync(
        int                         pageSize,
        int                         page,
        string?                     type         = null,
        string?                     textSearch   = null,
        TbDeviceSearchSortProperty? sortProperty = null,
        TbSortOrderDirection?       sortOrder    = null,
        CancellationToken           cancel       = default)
    {
        var policy = _builder.GetDefaultPolicy<TbPage<TbDevice>>()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("/api/tenant/devices")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder)
                .GetJsonAsync<TbPage<TbDevice>>(cancel);

            return response;
        });
    }
}
