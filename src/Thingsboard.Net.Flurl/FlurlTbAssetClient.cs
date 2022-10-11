using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbAssetClient : FlurlTbClient<ITbAssetClient>, ITbAssetClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbAssetClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Creates or Updates the Asset. When creating asset, platform generates Asset Id as time-based UUID. The newly created Asset id will be present in the response. Specify existing Asset id to update the asset. Referencing non-existing Asset Id will cause 'Not Found' error. Remove 'id', 'tenantId' and optionally 'customerId' from the request body example (below) to create new Asset entity.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="asset"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset> SaveAssetAsync(TbAsset asset, CancellationToken cancel = default)
    {
        if (asset == null) throw new ArgumentNullException(nameof(asset));

        var policy = _builder.GetPolicyBuilder<TbAsset>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/asset")
                .PostJsonAsync(asset, cancel)
                .ReceiveJson<TbAsset>();

            return response;
        });
    }

    /// <summary>
    /// Fetch the Asset object based on the provided Asset Id. If the user has the authority of 'Tenant Administrator', the server checks that the asset is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the asset is assigned to the same customer.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset?> GetAssetAsync(Guid assetId, CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder<TbAsset?>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/asset/{assetId}")
                .GetJsonAsync<TbAsset>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes the asset and all the relations (from and to the asset). Referencing non-existing asset Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteAssetAsync(Guid assetId, CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/asset/{assetId}")
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Fetch the Asset Info object based on the provided Asset Id. If the user has the authority of 'Tenant Administrator', the server checks that the asset is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the asset is assigned to the same customer. Asset Info is an extension of the default Asset object that contains information about the assigned customer name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAssetInfo?> GetAssetInfoAsync(Guid assetId, CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder<TbAssetInfo?>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/asset/info/{assetId}")
                .GetJsonAsync<TbAssetInfo>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a set of unique asset types based on assets that are either owned by the tenant or assigned to the customer which user is performing the request.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAssetType[]> GetAssetTypesAsync(CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder<TbAssetType[]>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, Array.Empty<TbAssetType>())
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/asset/types")
                .GetJsonAsync<TbAssetType[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns all assets that are related to the specific entity. The entity id, relation type, asset types, depth of the search, and other query parameters defined using complex 'AssetSearchQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset[]> FindByQueryAsync(TbAssetQueryRequest query, CancellationToken cancel = default)
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var policy = _builder.GetPolicyBuilder<TbAsset[]>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, Array.Empty<TbAsset>())
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/assets")
                .PostJsonAsync(query, cancel)
                .ReceiveJson<TbAsset[]>();

            return response;
        });
    }

    /// <summary>
    /// Requested assets must be owned by tenant or assigned to customer which user is performing the request.
    /// </summary>
    /// <param name="assetIds"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset[]> GetAssetsByIdsAsync(Guid[] assetIds, CancellationToken cancel = default)
    {
        if (assetIds == null) throw new ArgumentNullException(nameof(assetIds));
        if (assetIds.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(assetIds));

        var policy = _builder.GetPolicyBuilder<TbAsset[]>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, Array.Empty<TbAsset>())
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/assets")
                .SetQueryParam("assetIds", assetIds.JoinWith(","))
                .GetJsonAsync<TbAsset[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Creates assignment of the asset to customer. Customer will be able to query asset afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset> AssignAssetToCustomerAsync(Guid customerId, Guid assetId, CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder<TbAsset>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/asset/{assetId}/assignToCustomer/{customerId}")
                .PostJsonAsync(null, cancel)
                .ReceiveJson<TbAsset>();

            return response;
        });
    }

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
    public Task<TbPage<TbAssetInfo>> GetCustomerAssetInfosAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default)
    {
        var policy = _builder.GetPolicyBuilder<TbPage<TbAssetInfo>>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAssetInfo>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/customer/{customerId}/assetInfos")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbAssetInfo>>(cancel);
        });
    }

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
    public Task<TbPage<TbAsset>> GetCustomerAssetAsync(
        Guid                       customerId,
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default)
    {
        var policy = _builder.GetPolicyBuilder<TbPage<TbAsset>>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAsset>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/customer/{customerId}/assets")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbAsset>>(cancel);
        });
    }

    /// <summary>
    /// Clears assignment of the asset to customer. Customer will not be able to query asset afterwards.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset> UnassignAssetFromCustomerAsync(Guid assetId, CancellationToken cancel = default)
    {
        var policy = _builder.GetPolicyBuilder<TbAsset>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/customer/asset/{assetId}")
                .PostJsonAsync(null, cancel)
                .ReceiveJson<TbAsset>();

            return response;
        });
    }

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
    public Task<TbPage<TbAssetInfo>> GetTenantAssetInfosAsync(
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default)
    {
        var policy = _builder.GetPolicyBuilder<TbPage<TbAssetInfo>>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAssetInfo>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/tenant/assetInfos")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbAssetInfo>>(cancel);
        });
    }

    /// <summary>
    /// Requested asset must be owned by tenant that the user belongs to. Asset name is an unique property of asset. So it can be used to identify the asset.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetName">A string value representing the Asset name.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAsset?> GetTenantAssetAsync(string assetName, CancellationToken cancel = default)
    {
        if (string.IsNullOrEmpty(assetName)) throw new ArgumentException("Value cannot be null or empty.", nameof(assetName));

        var policy = _builder.GetPolicyBuilder<TbAsset?>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(CustomOptions)
                .AppendPathSegment($"/api/tenant/assets/{assetName}")
                .GetJsonAsync<TbAsset>(cancel);

            return response;
        });
    }

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
    public Task<TbPage<TbAsset>> GetTenantAssetAsync(
        int                        pageSize,
        int                        page,
        string?                    type         = null,
        string?                    textSearch   = null,
        TbAssetSearchSortProperty? sortProperty = null,
        TbSortOrder?               sortOrder    = null,
        CancellationToken          cancel       = default)
    {
        var policy = _builder.GetPolicyBuilder<TbPage<TbAsset>>(CustomOptions)
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAsset>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = _builder.CreateRequest(CustomOptions)
                .AppendPathSegment("/api/tenant/assets")
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("type",         type)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbAsset>>(cancel);
        });
    }
}
