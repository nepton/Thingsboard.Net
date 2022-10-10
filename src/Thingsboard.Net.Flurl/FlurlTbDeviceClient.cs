using System;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbDeviceClient : FlurlClientApi<ITbDeviceClient>, ITbDeviceClient
{
    private readonly IRequestBuilder _builder;

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbDeviceClient(IRequestBuilder builder)
    {
        _builder = builder;
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
        var policy = _builder.GetDefaultPolicy()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var result = await _builder.CreateRequest("/api/tenant/devices", GetCustomOptions())
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
            var response = await _builder.CreateRequest($"/api/device/{deviceId}", GetCustomOptions())
                .GetJsonAsync<TbDevice>(cancel);

            return response;
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
            var response = await _builder.CreateRequest($"/api/device/info/{deviceId}", GetCustomOptions())
                .GetJsonAsync<TbDeviceInfo>(cancel);

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
            var response = await _builder.CreateRequest($"/api/tenant/devices", GetCustomOptions())
                .SetQueryParam("deviceName", deviceName)
                .GetJsonAsync<TbDevice>(cancel);

            return response;
        });
    }
}
