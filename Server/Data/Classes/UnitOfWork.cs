using Data.Interfaces;
using NHibernate;

namespace Data.Classes;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;

    private ITransaction _transaction;

    private readonly Dictionary<Type, object> _repositories = [];

    public UnitOfWork(ISession session)
    {
        _session = session;
        _transaction = _session.BeginTransaction();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        if (!_repositories.ContainsKey(typeof(T)))
            _repositories[typeof(T)] = new Repository<T>(_session);
        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _session?.Dispose();
        GC.SuppressFinalize(this);
    }
}
