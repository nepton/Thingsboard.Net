using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

/// <summary>
///
/// </summary>
public interface ITbAssetClient : ITbClient<ITbAssetClient>
{
    /// <summary>
    /// Updates the Asset. Specify existing Asset id to update the asset. Referencing non-existing Asset Id will cause 'Not Found' error. Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new Asset entity.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset> SaveAssetAsync(TbAsset asset, CancellationToken cancel = default);

    /// <summary>
    /// Creates the Asset. When creating asset, platform generates Asset Id as time-based UUID. The newly created Asset id will be present in the response.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset> SaveAssetAsync(TbNewAsset asset, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Asset object based on the provided Asset Id. If the user has the authority of 'Tenant Administrator', the server checks that the asset is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the asset is assigned to the same customer.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset?> GetAssetByIdAsync(Guid assetId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the asset and all the relations (from and to the asset). Referencing non-existing asset Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteAssetAsync(Guid assetId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the asset and all the relations (from and to the asset). Referencing non-existing asset Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="throwIfNotExist"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteAssetAsync(Guid assetId, bool throwIfNotExist, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Asset Info object based on the provided Asset Id. If the user has the authority of 'Tenant Administrator', the server checks that the asset is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the asset is assigned to the same customer. Asset Info is an extension of the default Asset object that contains information about the assigned customer name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAssetInfo?> GetAssetInfoByIdAsync(Guid assetId, CancellationToken cancel = default);

    /// <summary>
    /// Returns a set of unique asset types based on assets that are either owned by the tenant or assigned to the customer which user is performing the request.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAssetType[]> GetAssetTypesAsync(CancellationToken cancel = default);

    /// <summary>
    /// Returns all assets that are related to the specific entity. The entity id, relation type, asset types, depth of the search, and other query parameters defined using complex 'AssetSearchQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset[]> FindByQueryAsync(TbAssetQueryRequest query, CancellationToken cancel = default);

    /// <summary>
    /// Requested assets must be owned by tenant or assigned to customer which user is performing the request.
    /// </summary>
    /// <param name="assetIds"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset[]> GetAssetsByIdsAsync(Guid[] assetIds, CancellationToken cancel = default);

    /// <summary>
    /// Creates assignment of the asset to customer. Customer will be able to query asset afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset> AssignAssetToCustomerAsync(Guid customerId, Guid assetId, CancellationToken cancel = default);

    /// <summary>
    /// Returns a page of assets info objects assigned to customer. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details. Asset Info is an extension of the default Asset object that contains information about the assigned customer name.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Asset type</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the asset name.</param>
    /// <param name="sortProperty">Property of entity to sort by Available values : createdTime, customerTitle, label, name, type</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbAssetInfo>> GetCustomerAssetInfosAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default);

    /// <summary>
    /// Returns a page of assets objects assigned to customer. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Asset type</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the asset name.</param>
    /// <param name="sortProperty">Property of entity to sort by Available values : createdTime, customerTitle, label, name, type</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbAsset>> GetCustomerAssetAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default);

    /// <summary>
    /// Clears assignment of the asset to customer. Customer will not be able to query asset afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset> UnassignAssetFromCustomerAsync(Guid assetId, CancellationToken cancel = default);

    /// <summary>
    /// Returns a page of assets info objects owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details. Asset Info is an extension of the default Asset object that contains information about the assigned customer name.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Asset type</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the asset name.</param>
    /// <param name="sortProperty">Property of entity to sort by Available values : createdTime, customerTitle, label, name, type</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbAssetInfo>> GetTenantAssetInfosAsync(
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default);

    /// <summary>
    /// Requested asset must be owned by tenant that the user belongs to. Asset name is an unique property of asset. So it can be used to identify the asset.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetName">A string value representing the Asset name.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbAsset?> GetTenantAssetByNameAsync(string assetName, CancellationToken cancel = default);

    /// <summary>
    /// Returns a page of assets owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="type">Asset type</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the asset name.</param>
    /// <param name="sortProperty">Property of entity to sort by Available values : createdTime, customerTitle, label, name, type</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbPage<TbAsset>> GetTenantAssetAsync(
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default);
}
