using System.Linq.Expressions;
using LastExamBackEndProject.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace LastExamBackEndProject.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity, TContext> where TEntity : BaseRepositoryEntity where TContext : DbContext
{
    protected readonly TContext Context;

    protected BaseRepository(TContext dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, CancellationToken cancellationToken = new CancellationToken())
    {
        var query = Context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync(cancellationToken);
        return await query.ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>()
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.CreatedDate = DateTime.Now;
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.LastModifiedDate = DateTime.Now;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>().CountAsync(cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null, CancellationToken cancellationToken = new CancellationToken())
    {
        return await FilterByString(Context.Set<TEntity>(), filterString).OrderByDescending(e => e.LastModifiedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    protected abstract IQueryable<TEntity> FilterByString(IQueryable<TEntity> query, string? filterString);
}