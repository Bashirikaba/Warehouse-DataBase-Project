using ApplicationData.Infrastructure;
using Business.Infrastructure;
using NHibernate;
using System.Linq.Dynamic.Core;

namespace ApplicationData.Shared;

public class Repository<T> : IRepository<T> where T : IEntity
{
    protected readonly ISession _session;

    public Repository(ISession session)
    {
        _session = session;
    }

    public T? GetByFieldAsync(string fieldName, string value)
    {
        return Query().Where($"{fieldName} == @0", value).FirstOrDefault();
        
    }

    public IQueryable<T> Query()
    {
        return _session.Query<T>();
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
        T entity = await _session.GetAsync<T>(id);
        if (entity != null)
        {
            await _session.DeleteAsync(entity);
        }
    }
}
