using BaseRepository.Models;
using System.Linq.Expressions;

namespace BaseRepository.Abstractions;

public interface IBaseRepository<TEntity> where TEntity : BaseRepositoryEntity
{
    Task<List<TEntity>> GetAllAsync(CancellationToken token);
    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
                                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
                                 CancellationToken token);
    Task<TEntity?> GetByIdAsync(int id, CancellationToken token);
    Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken token);
    Task UpdateAsync(TEntity entity, CancellationToken token);
    Task DeleteAsync(TEntity entity, CancellationToken token);
    Task<int> GetCountAsync(CancellationToken token);
    Task<List<TEntity>> GetFilteredBatchOfData(int pageSize,
                                               int page,
                                               string? filterString = null,
                                               CancellationToken token = new CancellationToken());
}
