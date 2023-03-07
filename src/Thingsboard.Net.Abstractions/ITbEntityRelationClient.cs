using System.Threading;
using System.Threading.Tasks;
using Thingsboard.Net.TbEntityRelationModels;

namespace Thingsboard.Net;

public interface ITbEntityRelationClient : ITbClient<ITbEntityRelationClient>
{
    /// <summary>
    /// Creates or updates a relation between two entities in the platform. Relations unique key is a combination of from/to entity id and relation type group and relation type.
    /// </summary>
    /// <param name="relation"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation> SaveRelationAsync(TbEntityRelation relation, CancellationToken cancel = default);

    /// <summary>
    /// Returns relation object between two specified entities if present. Otherwise throws exception.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation?> GetRelationAsync(TbEntityId from, TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Deletes a relation between two entities in the platform.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="relationType"></param>
    /// <param name="relationTypeGroup"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteRelationAsync(TbEntityId from, TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns all entities that are related to the specific entity. The entity id, relation type, entity types, depth of the search, and other query parameters defined using complex 'EntityRelationsQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="find">The find query</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation[]> FindByQueryAsync(TbEntityRelationFind find, CancellationToken cancel = default);

    /// <summary>
    /// Deletes all the relation (both 'from' and 'to' direction) for the specified entity.
    /// </summary>
    /// <param name="entityId"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task DeleteRelationsAsync(TbEntityId entityId, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation[]> FindByFromAsync(TbEntityId from, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation[]> FindByFromAsync(TbEntityId from, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationType">A string value representing relation type between entities. For example, 'Contains', 'Manages'. It can be any string value.</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation[]> FindByToAsync(TbEntityId to, string relationType, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelation[]> FindByToAsync(TbEntityId to, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns all entities that are related to the specific entity. The entity id, relation type, entity types, depth of the search, and other query parameters defined using complex 'EntityRelationsQuery' object. See 'Model' tab of the Parameters for more info.
    /// </summary>
    /// <param name="find">The find query</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelationInfo[]> FindInfoByQueryAsync(TbEntityRelationFind find, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'from' direction and relation type.
    /// </summary>
    /// <param name="from">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelationInfo[]> FindInfoByFromAsync(TbEntityId from, string? relationTypeGroup = null, CancellationToken cancel = default);

    /// <summary>
    /// Returns list of relation objects for the specified entity by the 'to' direction and relation type.
    /// </summary>
    /// <param name="to">A string value representing the entity id and entityType</param>
    /// <param name="relationTypeGroup">A string value representing relation type group. For example, 'COMMON'</param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<TbEntityRelationInfo[]> FindInfoByToAsync(TbEntityId to, string? relationTypeGroup = null, CancellationToken cancel = default);
}
