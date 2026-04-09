using ApplicationData.Infrastructure;
using NHibernate;
using NHibernate.Dialect.Function;

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

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _session.GetAsync<TEntity>(id);
    }


    public async Task<object?> InsertAsync(TEntity entity)
    {
        return await _session.SaveAsync(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await _session.UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        TEntity entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
