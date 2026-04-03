namespace API.Models.Tables;

/// <summary>
/// Склад
/// </summary>
public class Warehouse
{
    public virtual int Id { get; set; }

    public virtual Staff? Manager { get; set; }

    public virtual required string Name { get; set; }
}
