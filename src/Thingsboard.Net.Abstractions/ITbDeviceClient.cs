using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
/// The interface that the device queries from Thingsboard
/// </summary>
public interface ITbDeviceClient : ITbClient<ITbDeviceClient>
{
    /// <summary>
    /// Creates assignment of the device to customer. Customer will be able to query device afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDevice?> AssignDeviceToCustomerAsync(Guid customerId, Guid deviceId, CancellationToken cancel = default);

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
    Task<TbPage<TbDevice>> GetCustomerDeviceInfosAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type,
        Guid?                      deviceProfileId,
        string?                    textSearch,
        TbDeviceSearchSortProperty sortProperty,
        TbSortOrder       sortOrder,
        CancellationToken          cancel = default);

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
    Task<TbPage<TbDevice>> GetCustomerDevicesAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type,
        string?                    textSearch,
        TbDeviceSearchSortProperty sortProperty,
        TbSortOrder       sortOrder,
        CancellationToken          cancel = default);

    /// <summary>
    /// Clears assignment of the device to customer. Customer will not be able to query device afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDevice?> UnassignDeviceFromCustomerAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// Create or update the Device. When creating device, platform generates Device Id as time-based UUID. Device credentials are also generated if not provided in the 'accessToken' request parameter. The newly created device id will be present in the response. Specify existing Device id to update the device. Referencing non-existing device Id will cause 'Not Found' error.
    /// </summary>
    /// <param name="device">New device info</param>
    /// <param name="deviceAccessToken">Optional value of the device credentials to be used during device creation. If omitted, access token will be auto-generated.</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// Device name is unique in the scope of tenant. Use unique identifiers like MAC or IMEI for the device names and non-unique 'label' field for user-friendly visualization purposes.Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new Device entity.
    /// </remarks>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    Task<TbDevice> SaveDeviceAsync(TbDevice device, string? deviceAccessToken, CancellationToken cancel = default);

    /// <summary>
    /// Gets the specified device by Id. If the device does not exist, null is returned
    /// </summary>
    /// <param name="deviceId">device Id</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the device object, or null if it does not exist</returns>
    Task<TbDevice?> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the device, it's credentials and all the relations (from and to the device). Referencing non-existing device Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteDeviceAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Device Info object based on the provided Device Id. If the user has the authority of 'Tenant Administrator', the server checks that the device is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the device is assigned to the same customer. Device Info is an extension of the default Device object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceInfo?> GetDeviceInfoByIdAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// If during device creation there wasn't specified any credentials, platform generates random 'ACCESS_TOKEN' credentials.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceCredential?> GetDeviceCredentialsAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// During device creation, platform generates random 'ACCESS_TOKEN' credentials. Use this method to update the device credentials. First use 'getDeviceCredentialsByDeviceId' to get the credentials id and value. Then use current method to update the credentials type and value. It is not possible to create multiple device credentials for the same device. The structure of device credentials id and value is simple for the 'ACCESS_TOKEN' but is much more complex for the 'MQTT_BASIC' or 'LWM2M_CREDENTIALS'.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="credential"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceCredential> UpdateDeviceCredentialsAsync(TbDeviceCredential credential, CancellationToken cancel = default);

    /// <summary>
    /// Requested devices must be owned by tenant or assigned to customer which user is performing the request.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceIds">A list of devices ids</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDevice[]> GetDevicesByIds(Guid[] deviceIds, CancellationToken cancel = default);

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
    Task<TbPage<TbDeviceInfo>> GetTenantDeviceInfosAsync(
        int                         pageSize,
        int                         page,
        string?                     type            = null,
        Guid?                       deviceProfileId = null,
        string?                     textSearch      = null,
        TbDeviceSearchSortProperty? sortProperty    = null,
        TbSortOrder?       sortOrder       = null,
        CancellationToken           cancel          = default);

    /// <summary>
    /// Requested device must be owned by tenant that the user belongs to. Device name is an unique property of device. So it can be used to identify the device.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceName">A string value representing the Device name.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDevice?> GetTenantDeviceByNameAsync(string deviceName, CancellationToken cancel = default);

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
    Task<TbPage<TbDevice>> GetTenantDevicesAsync(
        int                         pageSize,
        int                         page,
        string?                     type         = null,
        string?                     textSearch   = null,
        TbDeviceSearchSortProperty? sortProperty = null,
        TbSortOrder?       sortOrder    = null,
        CancellationToken           cancel       = default);
}
