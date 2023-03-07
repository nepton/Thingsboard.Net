using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Thingsboard.Net.Exceptions;
using Thingsboard.Net.Flurl.Utilities;
using Thingsboard.Net.TbEntityRelationModels;

namespace Thingsboard.Net.Flurl;

public class FlurlTbEntityRelationClient : FlurlTbClient<ITbEntityRelationClient>, ITbEntityRelationClient
{
    public FlurlTbEntityRelationClient(IRequestBuilder builder) : base(builder)
    {
    }

    /// <summary>
    /// Creates or updates a relation between two entities in the platform. Relations unique key is a combination of from/to entity id and relation type group and relation type.
    /// </summary>
    /// <param name="relation"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation> SaveRelationAsync(TbEntityRelation relation, CancellationToken cancel = default)
    {
        if (relation == null) throw new ArgumentNullException(nameof(relation));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relation")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(relation, cancel)
                .ReceiveJson<TbEntityRelation>();

            return response;
        });
    }

    /// <summary>
    /// Returns relation object between two specified entities if present. Otherwise throws exception.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation?> GetRelationAsync(TbEntityId from, TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (relationType == null) throw new ArgumentNullException(nameof(relationType));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation?>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackValueOn(HttpStatusCode.NotFound, null)
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relation")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("fromId",            from.Id)
                .SetQueryParam("fromType",          from.EntityType)
                .SetQueryParam("relationType",      relationType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .SetQueryParam("toId",              to.Id)
                .SetQueryParam("toType",            to.EntityType)
                .GetJsonAsync<TbEntityRelation?>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Deletes a relation between two entities in the platform.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="relationType"></param>
    /// <param name="relationTypeGroup"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteRelationAsync(TbEntityId from, TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (relationType == null) throw new ArgumentNullException(nameof(relationType));

        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbRelationNotFoundException())
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment("api/relation")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("fromId",            from.Id)
                .SetQueryParam("fromType",          from.EntityType)
                .SetQueryParam("relationType",      relationType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .SetQueryParam("toId",              to.Id)
                .SetQueryParam("toType",            to.EntityType)
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Returns all entities that are related to the specific entity. The entity id, relation type, entity types, depth of the search, and other query parameters defined using complex 'EntityRelationsQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="find">The find query</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation[]> FindByQueryAsync(TbEntityRelationFind find, CancellationToken cancel = default)
    {
        if (find == null) throw new ArgumentNullException(nameof(find));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(find, cancel)
                .ReceiveJson<TbEntityRelation[]>();

            return response;
        });
    }

    /// <summary>
    /// Deletes all the relation (both 'from' and 'to' direction) for the specified entity.
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task DeleteRelationsAsync(TbEntityId entityId, CancellationToken cancel = default)
    {
        if (entityId == null) throw new ArgumentNullException(nameof(entityId));
        
        var policy = RequestBuilder.GetPolicyBuilder()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .FallbackOn(HttpStatusCode.NotFound, () => throw new TbRelationNotFoundException())
            .Build();
        
        return policy.ExecuteAsync(async builder =>
        {
            await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("entityId", entityId.Id)
                .SetQueryParam("entityType", entityId.EntityType)
                .DeleteAsync(cancel);
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation[]> FindByFromAsync(TbEntityId from, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));
        if (relationType == null) throw new ArgumentNullException(nameof(relationType));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("fromId",            from.Id)
                .SetQueryParam("fromType",          from.EntityType)
                .SetQueryParam("relationType",      relationType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelation[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation[]> FindByFromAsync(TbEntityId from, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("fromId",            from.Id)
                .SetQueryParam("fromType",          from.EntityType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelation[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation[]> FindByToAsync(TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (to == null) throw new ArgumentNullException(nameof(to));
        if (relationType == null) throw new ArgumentNullException(nameof(relationType));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("toId",              to.Id)
                .SetQueryParam("toType",            to.EntityType)
                .SetQueryParam("relationType",      relationType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelation[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelation[]> FindByToAsync(TbEntityId to, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (to == null) throw new ArgumentNullException(nameof(to));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelation[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("toId",              to.Id)
                .SetQueryParam("toType",            to.EntityType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelation[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns all entities that are related to the specific entity. The entity id, relation type, entity types, depth of the search, and other query parameters defined using complex 'EntityRelationsQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="find">The find query</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelationInfo[]> FindInfoByQueryAsync(TbEntityRelationFind find, CancellationToken cancel = default)
    {
        if (find == null) throw new ArgumentNullException(nameof(find));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelationInfo[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations/info")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .PostJsonAsync(find, cancel)
                .ReceiveJson<TbEntityRelationInfo[]>();

            return response;
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelationInfo[]> FindInfoByFromAsync(TbEntityId from, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (from == null) throw new ArgumentNullException(nameof(from));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelationInfo[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations/info")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("fromId",            from.Id)
                .SetQueryParam("fromType",          from.EntityType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelationInfo[]>(cancel);

            return response;
        });
    }

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    public Task<TbEntityRelationInfo[]> FindInfoByToAsync(TbEntityId to, string? relationTypeGroup = null, CancellationToken cancel = default)
    {
        if (to == null) throw new ArgumentNullException(nameof(to));

        var policy = RequestBuilder.GetPolicyBuilder<TbEntityRelationInfo[]>()
            .RetryOnHttpTimeout()
            .RetryOnUnauthorized()
            .Build();

        return policy.ExecuteAsync(async builder =>
        {
            var response = await builder.CreateRequest()
                .AppendPathSegment("api/relations/info")
                .WithOAuthBearerToken(await builder.GetAccessTokenAsync())
                .SetQueryParam("toId",              to.Id)
                .SetQueryParam("toType",            to.EntityType)
                .SetQueryParam("relationTypeGroup", relationTypeGroup)
                .GetJsonAsync<TbEntityRelationInfo[]>(cancel);

            return response;
        });
    }
}
