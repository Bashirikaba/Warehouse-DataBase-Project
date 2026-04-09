using ApplicationData.Infrastructure;
using Business.Infrastructure;
using NHibernate;

namespace ApplicationData.Shared;

public class UnitOfWork : IUnitOfWork
{
    private readonly ISession _session;
    private ITransaction? _transaction;
    private readonly Dictionary<Type, object> _repositories = [];

    public UnitOfWork(ISession session)
    {
        _session = session;
    }

    public void BeginTransaction()
    {
        if (_transaction == null || !_transaction.IsActive)
        {
            _transaction = _session.BeginTransaction();
        }
    }

    public IRepository<T> GetRepository<T>() where T : IEntity
    {
        if (!_repositories.ContainsKey(typeof(T)))
            _repositories[typeof(T)] = new Repository<T>(_session);
        return (IRepository<T>)_repositories[typeof(T)];
    }

    public async Task CommitAsync()
    {
        if (_transaction != null && _transaction.IsActive)
            await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null && _transaction.IsActive)
            await _transaction.RollbackAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _session?.Dispose();
        GC.SuppressFinalize(this);
    }
}