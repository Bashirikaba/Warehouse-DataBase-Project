namespace API.Models.Tables;

/// <summary>
/// Персонал
/// </summary>
public class Staff
{
    public virtual int Id { get; set; }

    public virtual required Warehouse Warehouse { get; set; }

    public virtual required Position Position { get; set; }

    public virtual required string FullName { get; set; }

    public virtual required string TIN { get; set; }
}
