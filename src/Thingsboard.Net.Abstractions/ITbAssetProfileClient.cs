using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary> The interface that the asset queries from Thingsboard </summary>
public interface ITbAssetProfileClient : ITbClient<ITbAssetProfileClient>
{
    /// <summary>
    /// Deletes the asset profile. Referencing non-existing asset profile Id will cause an error.
    /// Can't delete the asset profile if it is referenced by existing assets. Available for users
    /// with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetProfileId">
    /// A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'
    /// </param>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task<bool> DeleteAssetProfileAsync(Guid assetProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Default Asset Profile Info object. Asset Profile Info is a lightweight object
    /// that includes main information about Asset Profile excluding the heavyweight configuration
    /// object. Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task<TbAssetProfileInfo?> GetDefaultAssetProfileInfoAsync(CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Asset Profile object based on the provided Asset Profile Id. The server checks
    /// that the asset profile is owned by the same tenant.
    /// </summary>
    /// <param name="assetProfileId">
    /// A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'
    /// </param>
    /// <param name="cancel"> </param>
    /// <returns> Returns the asset object, or null if it does not exist </returns>
    Task<TbAssetProfile?> GetAssetProfileByIdAsync(Guid assetProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the AssetProfile Info object based on the provided AssetProfile Id. If the user has
    /// the authority of 'Tenant Administrator', the server checks that the asset is owned by the
    /// same tenant. If the user has the authority of 'Customer User', the server checks that the
    /// asset is assigned to the same customer. AssetProfile Info is an extension of the default
    /// AssetProfile object that contains information about the assigned customer name and asset
    /// profile name. Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetProfileId">
    /// A string value representing the asset id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'
    /// </param>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task<TbAssetProfileInfo?> GetAssetProfileInfoByIdAsync(Guid assetProfileId, CancellationToken cancel = default);

    /// <summary>
    /// Returns a page of assets profile info objects owned by tenant. You can specify parameters
    /// to filter the results. The result is wrapped with PageData object that allows you to iterate
    /// over result set using pagination. See the 'Model' tab of the Response Class for more
    /// details. Asset Profile Info is a lightweight object that includes main information about
    /// Asset Profile excluding the heavyweight configuration object.
    /// </summary>
    /// <param name="textSearch"> The search criteria </param>
    /// <param name="sortProperty"> Sort properties </param>
    /// <param name="sortOrder"> Sort direction </param>
    /// <param name="page"> Indicate which page will be query </param>
    /// <param name="pageSize"> The number of records read </param>
    /// <param name="transportType"> Type of the transport </param>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task<TbPage<TbAssetProfileInfo>> GetAssetProfileInfosAsync(
        int pageSize,
        int page,
        string? textSearch = null,
        TbAssetProfileSearchSortProperty? sortProperty = null,
        TbSortOrder? sortOrder = null,
        CancellationToken cancel = default);

    /// <summary> Obtaining the AssetProfile List </summary>
    /// <param name="textSearch"> The search criteria </param>
    /// <param name="sortProperty"> Sort properties </param>
    /// <param name="sortOrder"> Sort direction </param>
    /// <param name="page"> Indicate which page will be query </param>
    /// <param name="pageSize"> The number of records read </param>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task<TbPage<TbAssetProfile>> GetAssetProfilesAsync(
        int pageSize,
        int page,
        string? textSearch = null,
        TbAssetProfileSearchSortProperty? sortProperty = null,
        TbSortOrder? sortOrder = null,
        CancellationToken cancel = default);

    /// <summary>
    /// Create or update the Asset Profile. When creating asset profile, platform generates asset profile id as time-based UUID. The newly created asset profile id will be present in the response. Specify existing asset profile id to update the asset profile. Referencing non-existing asset profile Id will cause 'Not Found' error.
    /// Asset profile name is unique in the scope of tenant.Only one 'default' asset profile may exist in scope of tenant.
    /// </summary>
    /// <param name="assetProfile">New asset profile</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// AssetProfile name is unique in the scope of tenant. Use unique identifiers like MAC or IMEI for the asset names and non-unique 'label' field for user-friendly visualization purposes.Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new AssetProfile entity.
    /// </remarks>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    Task<TbAssetProfile> SaveAssetProfileAsync(TbNewAssetProfile assetProfile, CancellationToken cancel = default);

    /// <summary>
    /// Create or update the Asset Profile. When creating asset profile, platform generates asset profile id as time-based UUID. The newly created asset profile id will be present in the response. Specify existing asset profile id to update the asset profile. Referencing non-existing asset profile Id will cause 'Not Found' error.
    /// Asset profile name is unique in the scope of tenant.Only one 'default' asset profile may exist in scope of tenant.
    /// </summary>
    /// <param name="assetProfile">New asset info</param>
    /// <param name="cancel"></param>
    /// <remarks>
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    ///</remarks>
    /// <returns></returns>
    Task<TbAssetProfile> SaveAssetProfileAsync(TbAssetProfile assetProfile, CancellationToken cancel = default);

    /// <summary>
    /// Marks asset profile as default within a tenant scope. Available for users with
    /// 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetProfileId">
    /// A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'
    /// </param>
    /// <param name="cancel"> </param>
    /// <returns> </returns>
    Task SetDefaultAssetProfileAsync(Guid assetProfileId, CancellationToken cancel = default);
}