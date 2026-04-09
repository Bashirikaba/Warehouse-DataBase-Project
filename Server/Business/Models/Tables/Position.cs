namespace Business.Models;

/// <summary>
/// Должность
/// </summary>
public class Position
{
    public virtual int Id { get; set; }

    public virtual required string Name { get; set; }
}
