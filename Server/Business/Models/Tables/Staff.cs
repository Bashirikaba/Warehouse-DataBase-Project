using Business.Infrastructure;

namespace Business.Models;

/// <summary>
/// Персонал
/// </summary>
public class Staff : IEntity
{
    public virtual int Id { get; set; }

    public virtual required Warehouse Warehouse { get; set; }

    public virtual required Position Position { get; set; }

    public virtual required string FullName { get; set; }

    public virtual required string TIN { get; set; }
}
