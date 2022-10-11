using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.Flurl.Utilities.Implements;

namespace Thingsboard.Net.Flurl;

public class FlurlTbCustomerClient : FlurlTbClient<ITbCustomerClient>, ITbCustomerClient
{
    private readonly IRequestBuilder _builder;

    public FlurlTbCustomerClient(IRequestBuilder builder)
    {
        _builder = builder;
    }

    /// <summary>
    /// Creates or Updates the Customer. When creating customer, platform generates Customer Id as time-based UUID. The newly created Customer Id will be present in the response. Specify existing Customer Id to update the Customer. Referencing non-existing Customer Id will cause 'Not Found' error.Remove 'id', 'tenantId' from the request body example (below) to create new Customer entity.
    /// Available for users with 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="customer"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbCustomer> SaveCustomerAsync(TbCustomer customer, CancellationToken cancel = default)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));

        var policy = _builder.GetDefaultPolicy<TbCustomer>().RetryOnUnauthorized().Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment("/api/customer")
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
        var policy = _builder.GetDefaultPolicy<TbCustomer?>()
            .RetryOnUnauthorized()
            .FallbackToValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/customer/{customerId}")
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
        var policy = _builder.GetDefaultPolicy()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/customer/{customerId}")
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
        var policy = _builder.GetDefaultPolicy<TbCustomerShortInfo?>()
            .RetryOnUnauthorized()
            .FallbackToValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/customer/{customerId}/shortInfo")
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
        var policy = _builder.GetDefaultPolicy<string?>()
            .RetryOnUnauthorized()
            .FallbackToValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/customer/{customerId}/title")
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
        var policy = _builder.GetDefaultPolicy<TbPage<TbCustomer>>()
            .RetryOnUnauthorized()
            .FallbackToValueOn(HttpStatusCode.NotFound, TbPage<TbCustomer>.Empty)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var request = _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/customers")
                .SetQueryParam("pageSize", pageSize)
                .SetQueryParam("page",     page);

            if (!string.IsNullOrEmpty(textSearch))
                request = request.SetQueryParam("textSearch", textSearch);

            if (sortProperty.HasValue)
                request = request.SetQueryParam("sortProperty", sortProperty);

            if (sortOrder.HasValue)
                request = request.SetQueryParam("sortOrder", sortOrder);

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
        var policy = _builder.GetDefaultPolicy<TbCustomer?>()
            .RetryOnUnauthorized()
            .FallbackToValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async () =>
        {
            var response = await _builder.CreateRequest(GetCustomOptions())
                .AppendPathSegment($"/api/tenant/customers/{customerTitle}")
                .GetJsonAsync<TbCustomer>(cancel);

            return response;
        });
    }
}
