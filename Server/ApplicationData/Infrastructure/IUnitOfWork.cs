using Business.Infrastructure;

namespace ApplicationData.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : IEntity;
    void BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
}
