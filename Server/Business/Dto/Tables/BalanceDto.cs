using Business.Models;

namespace Business.Dto;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class BalanceDto
{
    public int? Id { get; set; }
    
    public required Warehouse Warehouse { get; set; }

    public required Good Good { get; set; }

    public virtual int Quantity { get; set; }
}
