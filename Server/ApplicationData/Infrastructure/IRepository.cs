using Business.Infrastructure;

namespace ApplicationData.Infrastructure;

public interface IRepository<T> where T : IEntity
{
    IQueryable<T> Query();

    Task<T> GetById(int id);

    Task<object?> InsertAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteByIdAsync(int id);
}