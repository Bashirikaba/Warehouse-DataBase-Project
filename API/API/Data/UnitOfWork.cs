using NHibernate;

namespace API.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly NHibernate.ISession _session;
    private ITransaction _transaction;

    public UnitOfWork(ISessionFactory sessionFactory)
    {
        _session = sessionFactory.OpenSession();
        _transaction = _session.BeginTransaction();
    }

    public IQueryable<T> Query<T>() where T : class
    {
        return _session.Query<T>();
    }

    public void Add<T>(T entity) where T : class
    {
        _session.Save(entity);
    }

    public void Update<T>(T entity) where T : class
    {
        _session.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _session.Delete(entity);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _transaction.CommitAsync(cancellationToken);
            return 1;
        }
        catch
        {
            await _transaction.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _session?.Dispose();
        GC.SuppressFinalize(this);
    }
}