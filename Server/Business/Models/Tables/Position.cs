using Business.Infrastructure;

namespace Business.Models;

/// <summary>
/// Должность
/// </summary>
public class Position : IEntity
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; }
}
