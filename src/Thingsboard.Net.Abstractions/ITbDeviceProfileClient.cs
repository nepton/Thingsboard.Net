using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
/// The interface that the device queries from Thingsboard
/// </summary>
public interface ITbDeviceProfileClient : ITbClient<ITbDeviceProfileClient>
{
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
    Task<TbDeviceProfile> SaveDeviceProfileAsync(TbNewDeviceProfile deviceProfile, CancellationToken cancel = default);

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
    Task<TbDeviceProfile> SaveDeviceProfileAsync(TbDeviceProfile deviceProfile, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Device Profile object based on the provided Device Profile Id. The server checks that the device profile is owned by the same tenant.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the device object, or null if it does not exist</returns>
    Task<TbDeviceProfile?> GetDeviceProfileByIdAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the device profile. Referencing non-existing device profile Id will cause an error. Can't delete the device profile if it is referenced by existing devices.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteDeviceProfileAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the device profile. Referencing non-existing device profile Id will cause an error. Can't delete the device profile if it is referenced by existing devices.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="throwIfNotExist">If true, throw an exception if the device profile does not exist</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteDeviceProfileAsync(Guid deviceProfileId, bool throwIfNotExist, CancellationToken cancel = default);

    /// <summary>
    /// Marks device profile as default within a tenant scope.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task SetDefaultDeviceProfileAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Get a set of unique attribute keys used by devices that belong to specified profile. If profile is not set returns a list of unique keys among all profiles. The call is used for auto-complete in the UI forms. The implementation limits the number of devices that participate in search to 100 as a trade of between accurate results and time-consuming queries.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string[]> GetAttributeKeysAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Get a set of unique time-series keys used by devices that belong to specified profile. If profile is not set returns a list of unique keys among all profiles. The call is used for auto-complete in the UI forms. The implementation limits the number of devices that participate in search to 100 as a trade of between accurate results and time-consuming queries.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the entity id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<string[]> GetTimeSeriesKeysAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the DeviceProfile Info object based on the provided DeviceProfile Id. If the user has the authority of 'Tenant Administrator', the server checks that the device is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the device is assigned to the same customer. DeviceProfile Info is an extension of the default DeviceProfile object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceProfileId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceProfileInfo?> GetDeviceProfileInfoByIdAsync(Guid deviceProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Default Device Profile Info object. Device Profile Info is a lightweight object that includes main information about Device Profile excluding the heavyweight configuration object.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceProfileInfo?> GetDefaultDeviceProfileInfoAsync(CancellationToken cancel = default);

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
    Task<TbPage<TbDeviceProfileInfo>> GetDeviceProfileInfosAsync(
        int                                pageSize,
        int                                page,
        string?                            textSearch    = null,
        TbDeviceProfileSearchSortProperty? sortProperty  = null,
        TbSortOrder?                       sortOrder     = null,
        TbDeviceProfileTransportType?      transportType = null,
        CancellationToken                  cancel        = default);

    /// <summary>
    /// Obtaining the DeviceProfile List
    /// </summary>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbDeviceProfile>> GetDeviceProfilesAsync(
        int                                pageSize,
        int                                page,
        string?                            textSearch   = null,
        TbDeviceProfileSearchSortProperty? sortProperty = null,
        TbSortOrder?                       sortOrder    = null,
        CancellationToken                  cancel       = default);
}
