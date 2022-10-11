using System;
using System.Threading;
using System.Threading.Tasks;

namespace Thingsboard.Net;

public interface ITbQueueClient : ITbClient<ITbQueueClient>
{
    /// <summary>
    /// Returns a page of queues registered in the platform. You can specify parameters to filter the results. The result is wrapped with PageData object that allows you to iterate over result set using pagination. See the 'Model' tab of the Response Class for more details.
    /// Available for users with 'SYS_ADMIN' or 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="serviceType">Service type (implemented only for the TB-RULE-ENGINE)</param>
    /// <param name="pageSize">Maximum amount of entities in a one page</param>
    /// <param name="page">Sequence number of page starting from 0</param>
    /// <param name="textSearch">The case insensitive 'substring' filter based on the queue name.</param>
    /// <param name="sortProperty">Property of entity to sort by</param>
    /// <param name="sortOrder">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TbPage<TbQueue>> GetTenantQueuesByServiceTypeAsync(
        TbQueueServiceType        serviceType,
        int                       pageSize,
        int                       page,
        string?                   textSearch        = null,
        TbQueueQuerySortProperty? sortProperty      = null,
        TbSortOrder?              sortOrder         = null,
        CancellationToken         cancellationToken = default);

    /// <summary>
    /// Create or update the Queue. When creating queue, platform generates Queue Id as time-based UUID. Specify existing Queue id to update the queue. Referencing non-existing Queue Id will cause 'Not Found' error.
    /// Queue name is unique in the scope of sysadmin.Remove 'id', 'tenantId' from the request body example (below) to create new Queue entity.
    /// Available for users with 'SYS_ADMIN' authority.
    /// </summary>
    /// <param name="serviceType">Sort order. ASC (ASCENDING) or DESC (DESCENDING)</param>
    /// <param name="queue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TbQueue> SaveQueueAsync(TbQueueServiceType serviceType, TbQueue queue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetch the Queue object based on the provided Queue Id.
    /// Available for users with 'SYS_ADMIN' or 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="queueId">A string value representing the queue id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbQueue?> GetQueueByIdAsync(Guid queueId, CancellationToken cancel = default);

    /// <summary>
    /// Deletes the Queue.
    /// Available for users with 'SYS_ADMIN' authority.
    /// </summary>
    /// <param name="queueId">A string value representing the queue id. For example, '784f394c-42b6-435a-983c-b7beff2784f9'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteQueueAsync(Guid queueId, CancellationToken cancel = default);

    /// <summary>
    /// Fetch the Queue object based on the provided Queue name.
    /// Available for users with 'SYS_ADMIN' or 'TENANT_ADMIN' authority.
    /// </summary>
    /// <param name="queueName">A string value representing the queue name. For example, 'Main'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbQueue?> GetQueueByNameAsync(string queueName, CancellationToken cancel = default);
}