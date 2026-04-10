using ApplicationData.Infrastructure;
using Business.Infrastructure;
using NHibernate;

namespace ApplicationData.Shared;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : IReport
{
    protected readonly ISession _session;

    public ReadOnlyRepository(ISession session)
    {
        _session = session;
    }

    public IQueryable<T> Query()
    {
        return _session.Query<T>();
    }
}
