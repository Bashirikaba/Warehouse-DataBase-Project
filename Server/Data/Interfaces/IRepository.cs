namespace Data.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Query();

    Task<TEntity> GetByIdAsync(int Id);

    Task InsertAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
