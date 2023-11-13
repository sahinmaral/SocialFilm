using Microsoft.EntityFrameworkCore;

using SocialFilm.Domain.Common;
using SocialFilm.Domain.Repositories;

using System.Linq.Expressions;

namespace SocialFilm.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity, TContext> : IRepository<TEntity>
where TEntity : class,IEntity, new()
where TContext : DbContext
{
    private readonly TContext _context;
    private DbSet<TEntity> Entity;

    public GenericRepository(TContext context)
    {
        _context = context;
        Entity = _context.Set<TEntity>();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await Entity.AnyAsync(expression,cancellationToken);
    }

    public bool Any(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.Any(expression);
    }

    public void Add(TEntity entity)
    {
        Entity.Add(entity);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entity.AddAsync(entity, cancellationToken);
    }

    public void AddRange(ICollection<TEntity> entities)
    {
        Entity.AddRange(entities);
    }

    public async Task AddRangeAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await Entity.AddRangeAsync(entities, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        Entity.Remove(entity);
    }

    public async Task DeleteByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        Entity.Remove(entity);
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        Entity.RemoveRange(entities);
    }

    public IQueryable<TEntity> GetAll()
    {
        return Entity.AsNoTracking().AsQueryable();
    }

    public TEntity? GetByExpression(Expression<Func<TEntity, bool>> expression)
    {
        TEntity? entity = Entity.Where(expression).AsNoTracking().FirstOrDefault();
        return entity;
    }

    public async Task<TEntity?> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await Entity.Where(expression).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    public TEntity? GetFirst()
    {
        TEntity entity = Entity.AsNoTracking().FirstOrDefault();
        return entity;
    }

    public async Task<TEntity?> GetFirstAsync(CancellationToken cancellationToken = default)
    {
        TEntity? entity = await Entity.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        return entity;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
    {
        return Entity.AsNoTracking().Where(expression).AsQueryable();
    }

    public virtual void Update(TEntity entity)
    {
        Entity.Update(entity);
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        Entity.UpdateRange(entities);
    }

    public async Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Entity.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public TEntity? GetByExpression(string id)
    {
        return Entity.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
    }
}
