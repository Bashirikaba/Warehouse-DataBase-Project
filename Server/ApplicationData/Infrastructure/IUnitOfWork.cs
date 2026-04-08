namespace ApplicationData.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> GetRepository<T>() where T : class;
    void BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
}
