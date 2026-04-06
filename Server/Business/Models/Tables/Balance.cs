namespace Business.Models;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class Balance
{
    public virtual int Id { get; set; }

    public virtual required Warehouse Warehouse { get; set; }

    public virtual required Good Good { get; set; }

    public virtual int Quantity { get; set; }
}
