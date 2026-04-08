using ApplicationData.Infrastructure;
using NHibernate;

namespace ApplicationData.Shared;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public IQueryable<TEntity> Query()
    {
        return _session.Query<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int Id)
    {
        return await _session.GetAsync<TEntity>(Id);
    }


    public async Task InsertAsync(TEntity entity)
    {
        await _session.SaveAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _session.UpdateAsync(entity);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await _session.DeleteAsync(entity);
    }
}
