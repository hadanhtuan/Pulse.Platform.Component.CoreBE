using Pulse.Library.Core.Persistence.EntityTypes;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;


/// Collection-like interface for accessing domain objects in persistent storage layer with primary keys set to Guids as default.
/// Provides operations similar to many of those defined 

/// <typeparam name="TAggregateRoot"></typeparam>
public interface IRepository<TAggregateRoot> : IRepository<TAggregateRoot, Guid>
    where TAggregateRoot : IAggregateRoot
{
}


/// Generic, Collection-like interface for accessing domain objects in persistent storage layer.
/// Provides operations similar to many of those defined 

/// <typeparam name="TAggregateRoot"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TAggregateRoot, TKey>
    where TKey : struct
    where TAggregateRoot : IAggregateRoot<TKey>
{
    void Add(TAggregateRoot entity);

    void Remove(TAggregateRoot entity);

    TAggregateRoot? SingleOrDefault(
        Expression<Func<TAggregateRoot, bool>> predicate,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    TProjection? SingleOrDefault<TProjection>(
        Expression<Func<TAggregateRoot, bool>> predicate,
        Expression<Func<TAggregateRoot, TProjection>> projection,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    TAggregateRoot Single(
        Expression<Func<TAggregateRoot, bool>> predicate,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    TProjection Single<TProjection>(
        Expression<Func<TAggregateRoot, bool>> predicate,
        Expression<Func<TAggregateRoot, TProjection>> projection,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    IQueryable<TAggregateRoot> Many(
        Expression<Func<TAggregateRoot, bool>>? predicate = null,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    IQueryable<TProjection> Many<TProjection>(
        Expression<Func<TAggregateRoot, bool>> predicate,
        Expression<Func<TAggregateRoot, TProjection>> projection,
        params Expression<Func<TAggregateRoot, object>>[] includes);

    bool Exists(Expression<Func<TAggregateRoot, bool>>? predicate = null);

    int Count(Expression<Func<TAggregateRoot, bool>>? predicate = null);
}