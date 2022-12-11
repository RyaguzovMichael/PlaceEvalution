using BaseRepository.Abstractions;
using BaseRepository.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BaseRepository;

public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
                                                          where TEntity : BaseRepositoryEntity
                                                          where TContext : DbContext
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly TContext Context;

    protected BaseRepository(TContext dbContext)
    {
        Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        DbSet = Context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken token)
    {
        return await DbSet.ToListAsync(token);
    }

    public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                                      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                      CancellationToken token = new CancellationToken())
    {
        var query = DbSet.AsNoTracking()
                         .Where(predicate);
        if (orderBy != null) return await orderBy(query).ToListAsync(token);
        return await query.ToListAsync(token);
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken token)
    {
        return await DbSet.FirstOrDefaultAsync(i => i.Id == id, token);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
    {
        entity.CreatedDate = DateTime.Now;
        DbSet.Add(entity);
        await Context.SaveChangesAsync(token);
        return entity;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken token)
    {
        entity.LastModifiedDate = DateTime.Now;
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(token);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync(token);
    }

    public virtual async Task<int> GetCountAsync(CancellationToken token)
    {
        return await DbSet.CountAsync(token);
    }

    public virtual async Task<List<TEntity>> GetFilteredBatchOfData(int pageSize, int page, string? filterString = null, CancellationToken token = new CancellationToken())
    {
        return await FilterByString(DbSet, filterString).OrderByDescending(e => e.LastModifiedDate)
                                                                         .Skip((page - 1) * pageSize)
                                                                         .Take(pageSize)
                                                                         .ToListAsync(token);
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
    {
        return await DbSet.FirstOrDefaultAsync(predicate, token);
    }

    protected abstract IQueryable<TEntity> FilterByString(IQueryable<TEntity> query, string? filterString);

}