using Business.Infrastructure;

namespace ApplicationData.Infrastructure;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> Query();

    Task<TEntity> GetByIdAsync(int id);

    Task<object?> InsertAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteByIdAsync(int id);
}
