using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;

namespace Thingsboard.Net.Flurl;

public class FlurlTbCustomerClient : FlurlTbClient<ITbCustomerClient>, ITbCustomerClient
{
    public FlurlTbCustomerClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Updates the Customer. Specify existing Customer Id to update the Customer. Referencing non-existing Customer Id will cause 'Not Found' error.Remove 'id', 'tenantId' from the request body example (below) to create new Customer entity.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomer> SaveCustomerAsync(TbCustomer customer, CancellationToken cancel = default)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));


        var policy = RequestBuilder.GetPolicyBuilder<TbCustomer>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbEntityNotFoundException(customer.Id))
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/customer")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(customer, cancel)
                .ReceiveJson<TbCustomer>();

            return response;
        });
    }

    /// <summary>
    /// Creates the Customer. When creating customer, platform generates Customer Id as time-based UUID. The newly created Customer Id will be present in the response. 
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomer> SaveCustomerAsync(TbNewCustomer customer, CancellationToken cancel = default)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));


        var policy = RequestBuilder.GetPolicyBuilder<TbCustomer>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("/api/customer")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(customer, cancel)
                .ReceiveJson<TbCustomer>();

            return response;
        });
    }

    /// <summary>
    /// Get the Customer object based on the provided Customer Id. If the user has the authority of 'Tenant Administrator', the server checks that the customer is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the user belongs to the customer.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomer?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbCustomer?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/customer/{customerId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbCustomer>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes the Customer and all customer Users. All assigned Dashboards, Assets, Devices, etc. will be unassigned but not deleted. Referencing non-existing Customer Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteCustomerAsync(Guid customerId, CancellationToken cancel = default)
    {
        return DeleteCustomerAsync(customerId, true, cancel);
    }

    /// <summary>
    /// Deletes the Customer and all customer Users. All assigned Dashboards, Assets, Devices, etc. will be unassigned but not deleted. Referencing non-existing Customer Id will cause an error.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="throwIfNotExist">Indicates whether to throw an exception if the customer does not exist.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteCustomerAsync(Guid customerId, bool throwIfNotExist, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throwIfNotExist ? throw new TbEntityNotFoundException(TbEntityType.CUSTOMER, customerId) : Task.CompletedTask)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment($"/api/customer/{customerId}")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Get the short customer object that contains only the title and 'isPublic' flag. If the user has the authority of 'Tenant Administrator', the server checks that the customer is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the user belongs to the customer.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomerShortInfo?> GetCustomerShortInfoAsync(Guid customerId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbCustomerShortInfo?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/customer/{customerId}/shortInfo")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetJsonAsync<TbCustomerShortInfo>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Get the title of the customer. If the user has the authority of 'Tenant Administrator', the server checks that the customer is owned by the same tenant. If the user has the authority of 'Customer User', the server checks that the user belongs to the customer.
    /// Available for users with 'TENANT_ADMIN' or 'CUSTOMER_USER' authority.
    /// </summary>
    /// <param name="customerId">A string value representing the customer id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<string?> GetCustomerTitleAsync(Guid customerId, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<string?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/customer/{customerId}/title")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .GetStringAsync(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns a page of customers owned by tenant. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the asset name.</param>
    /// <param name="sortProperty">Property of entity to sort</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbPage<TbCustomer>> GetCustomersAsync(
        int                     pageSize,
        int                     page,
        string?                 textSearch   = null,
        TbCustomerSortProperty? sortProperty = null,
        TbSortOrder?            sortOrder    = null,
        CancellationToken       cancel       = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbPage<TbCustomer>>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, TbPage<TbCustomer>.Empty)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var request = builder.CreateRequest()
                .AppendPathSegment($"/api/customers")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("pageSize",     pageSize)
                .SetQueryParam("page",         page)
                .SetQueryParam("textSearch",   textSearch)
                .SetQueryParam("sortProperty", sortProperty)
                .SetQueryParam("sortOrder",    sortOrder);

            return await request.GetJsonAsync<TbPage<TbCustomer>>(cancel);
        });
    }

    /// <summary>
    /// Get the Customer using Customer Title.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customerTitle">A string value representing the Customer title.</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomer?> GetTenantCustomerAsync(string customerTitle, CancellationToken cancel = default)
    {
        var policy = RequestBuilder.GetPolicyBuilder<TbCustomer?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment($"/api/tenant/customers")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("customerTitle", customerTitle)
                .GetJsonAsync<TbCustomer>(cancel);

            return response;
        });
    }
}
