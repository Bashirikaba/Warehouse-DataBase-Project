using ApplicationData.Infrastructure;
using Business.Infrastructure;
using NHibernate;

namespace ApplicationData.Shared;

public class Repository<T> : IRepository<T> where T : IEntity
{
    protected readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public IQueryable<T> Query()
    {
        return _session.Query<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _session.GetAsync<T>(id);
    }


    public async Task<object?> InsertAsync(T entity)
    {
        return await _session.SaveAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        await _session.UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        T entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
