using System.Linq.Expressions;

namespace Pulse.Library.Persistence.Repositories;

public interface IRepository<TEntity>
{
    void Add(TEntity entity);

    void Remove(TEntity entity);

    TEntity? SingleOrDefault(
        Expression<Func<TEntity, bool>> predicate, 
        params Expression<Func<TEntity, object>>[] includes);

    TProjection? SingleOrDefault<TProjection>(
        Expression<Func<TEntity, bool>> predicate, 
        Expression<Func<TEntity, TProjection>> projection, 
        params Expression<Func<TEntity, object>>[] includes);

    TEntity Single(
        Expression<Func<TEntity, bool>> predicate, 
        params Expression<Func<TEntity, object>>[] includes);

    TProjection Single<TProjection>(
        Expression<Func<TEntity, bool>> predicate, 
        Expression<Func<TEntity, TProjection>> projection, 
        params Expression<Func<TEntity, object>>[] includes);

    IQueryable<TEntity> Many(
        Expression<Func<TEntity, bool>>? predicate = null, 
        params Expression<Func<TEntity, object>>[] includes);

    IQueryable<TProjection> Many<TProjection>(
        Expression<Func<TEntity, bool>> predicate, 
        Expression<Func<TEntity, TProjection>> projection, 
        params Expression<Func<TEntity, object>>[] includes);

    bool Exists(Expression<Func<TEntity, bool>>? predicate = null);

    int Count(Expression<Func<TEntity, bool>>? predicate = null);
}