using Microsoft.EntityFrameworkCore;
using Pulse.Library.Core.Schema;
using System.Linq.Expressions;

namespace Pulse.Library.Persistence.Repositories;

/// Generic implementation of <see cref="IRepository{TEntity, TKey}"/> backed
/// by a DbContext.
public class Repository<TEntity, TKey> where TEntity : Entity
{
    private readonly DbContext context;

    public Repository(DbContext context)
    {
        this.context = context;
    }
    
    public void Add(Entity entity)
    {
        context.Set<Entity>().Add(entity);
    }
    
    public void Remove(Entity entity)
    {
        context.Set<Entity>().Remove(entity);
    }
    
    public TEntity? SingleOrDefault(
        Expression<Func<TEntity, bool>> predicate, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var set = CreateIncludedSet(includes);
        
        return set.SingleOrDefault(predicate);
    }
    
    public TProjection? SingleOrDefault<TProjection>(
        Expression<Func<TEntity, bool>> predicate, 
        Expression<Func<TEntity, TProjection>> projection, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var set = CreateIncludedSet(includes);
        
        return set.Where(predicate).Select(projection).SingleOrDefault();
    }
    
    private IQueryable<TEntity> CreateIncludedSet(params Expression<Func<TEntity, object>>[] includes)
    {
        var set = context.Set<TEntity>().AsQueryable();
        
        foreach (var include in includes)
        {
            set = set.Include(include);
        }
        
        return set;
    }
    
    public void Save(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }
}