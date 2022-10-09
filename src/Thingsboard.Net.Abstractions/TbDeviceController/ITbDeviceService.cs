using System;
using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.Common;
using Thingsboard.Net.Models;

namespace Thingsboard.Net.TbDeviceController;

/// <summary>
/// The interface that the device queries from Thingsboard
/// </summary>
public interface ITbDeviceApi : IClientApi<ITbDeviceApi>
{
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
        string?               type         = null,
        string?               textSearch   = null,
        string?               sortProperty = null,
        TbSortOrderDirection? sortOrder    = null,
        int                   page         = 0,
        int                   pageSize     = 20,
        CancellationToken     cancel       = default);

    /// <summary>
    /// Gets the specified device by Id. If the device does not exist, null is returned
    /// </summary>
    /// <param name="deviceId">device Id</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the device object, or null if it does not exist</returns>
    Task<TbDevice?> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Device Info object based on the provided Device Id. If the user has the authority of 'Tenant Administrator', the server checks that the device is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the device is assigned to the same customer. Device Info is an extension of the default Device object that contains information about the assigned customer name and device profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="deviceId">A string value representing the device id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbDeviceInfo?> GetDeviceInfoByIdAsync(Guid deviceId, CancellationToken cancel = default);
}
