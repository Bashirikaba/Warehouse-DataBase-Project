using Business.Infrastructure;

namespace ApplicationData.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : IEntity;

    IReadOnlyRepository<T> GetReadOnlyRepository<T>() where T : IReport;
    
    void BeginTransaction();
    
    Task CommitAsync();
    
    Task RollbackAsync();
}
