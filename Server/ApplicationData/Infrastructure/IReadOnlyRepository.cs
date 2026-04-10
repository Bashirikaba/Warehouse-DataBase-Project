using Business.Infrastructure;

namespace ApplicationData.Infrastructure;

public interface IReadOnlyRepository<T> where T : IReport
{
    IQueryable<T> Query();
}
