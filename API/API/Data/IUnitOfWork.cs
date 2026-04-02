using System;

namespace API.Data;

public interface IUnitOfWork : IDisposable
{
    IQueryable<T> Query<T>() where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
