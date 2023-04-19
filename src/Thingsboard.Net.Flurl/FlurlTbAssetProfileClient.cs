using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

/// <summary>
/// Thingsboard asset controller implements by flurl
/// </summary>
public class FlurlTbAssetProfileClient : FlurlTbClient<ITbAssetProfileClient>, ITbAssetProfileClient
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public FlurlTbAssetProfileClient(IRequestBuilder builder) : base(builder)
    {
    }

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
    public Task<TbAssetProfile> SaveAssetProfileAsync(TbNewAssetProfile assetProfile, CancellationToken cancel = default)
    {
        if (assetProfile == null) throw new ArgumentNullException(nameof(assetProfile));

        var policy = RequestBuilder.GetPolicyBuilder<TbAssetProfile>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/assetProfile")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(assetProfile, cancel)
                .ReceiveJson<TbAssetProfile>();

            return response;
        });
    }

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
    public Task<TbAssetProfile> SaveAssetProfileAsync(TbAssetProfile assetProfile, CancellationToken cancel = default)
    {
        if (assetProfile == null) throw new ArgumentNullException(nameof(assetProfile));

        var policy = RequestBuilder.GetPolicyBuilder<TbAssetProfile>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(assetProfile.Id))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/assetProfile")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(assetProfile, cancel)
                .ReceiveJson<TbAssetProfile>();

            return response;
        });
    }

    /// <summary>
    /// Fetch the Asset Profile object based on the provided Asset Profile Id. The server checks that the asset profile is owned by the same tenant.
    /// </summary>
    /// <param name="assetProfileId">A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns>Returns the asset object, or null if it does not exist</returns>
    public Task<TbAssetProfile?> GetAssetProfileByIdAsync(Guid assetProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbAssetProfile?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfile/{assetProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbAssetProfile?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes the asset profile. Referencing non-existing asset profile Id will cause an error. Can't delete the asset profile if it is referenced by existing assets.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetProfileId">A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<bool> DeleteAssetProfileAsync(Guid assetProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<bool>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, false)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfile/{assetProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);

            return true;
        });
    }

    /// <summary>
    /// Marks asset profile as default within a tenant scope.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="assetProfileId">A string value representing the asset profile id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task SetDefaultAssetProfileAsync(Guid assetProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(TbEntityType.DEVICE_PROFILE, assetProfileId))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfile/{assetProfileId}/default")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostAsync(null, cancel);
        });
    }

    /// <summary>
    /// Fetch the AssetProfile Info object based on the provided AssetProfile Id. If the user has the authority of 'Tenant Administrator', the server checks that the asset is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the asset is assigned to the same customer. AssetProfile Info is an extension of the default AssetProfile object that contains information about the assigned customer name and asset profile name.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="assetProfileId">A string value representing the asset id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAssetProfileInfo?> GetAssetProfileInfoByIdAsync(Guid assetProfileId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbAssetProfileInfo?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfileInfo/{assetProfileId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbAssetProfileInfo?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Fetch the Default Asset Profile Info object. Asset Profile Info is a lightweight object that includes main information about Asset Profile excluding the heavyweight configuration object.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbAssetProfileInfo?> GetDefaultAssetProfileInfoAsync(CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbAssetProfileInfo?>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfileInfo/default")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbAssetProfileInfo?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a page of assets profile info objects owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details. Asset Profile Info is a lightweight object that includes main information about Asset Profile excluding the heavyweight configuration object.
    /// </summary>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="transportType">Type of the transport</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAssetProfileInfo>> GetAssetProfileInfosAsync(
        int pageSize,
        int page,
        string? textSearch = null,
        TbAssetProfileSearchSortProperty? sortProperty = null,
        TbSortOrder? sortOrder = null,
        CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbAssetProfileInfo>>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAssetProfileInfo>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfileInfos")
                .SetQueryParam("pageSize", pageSize)
                .SetQueryParam("page", page)
                .SetQueryParam("textSearch", textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder", sortOrder)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbPage<TbAssetProfileInfo>>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Obtaining the AssetProfile List
    /// </summary>
    /// <param name="type">Type of asset</param>
    /// <param name="textSearch">The search criteria</param>
    /// <param name="sortProperty">Sort properties</param>
    /// <param name="sortOrder">Sort direction</param>
    /// <param name="page">Indicate which page will be query</param>
    /// <param name="pageSize">The number of records read</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbAssetProfile>> GetAssetProfilesAsync(
        int pageSize,
        int page,
        string? textSearch = null,
        TbAssetProfileSearchSortProperty? sortProperty = null,
        TbSortOrder? sortOrder = null,
        CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbAssetProfile>>()
            .RetryOnUnauthorized()
            .RetryOnHttpTimeout()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbAssetProfile>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/assetProfiles")
                .SetQueryParam("pageSize", pageSize)
                .SetQueryParam("page", page)
                .SetQueryParam("textSearch", textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder", sortOrder)
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbPage<TbAssetProfile>>(cancel);

            return response;
        });
    }
}
