namespace Business.Dto;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class BalanceDto
{
    public int? Id { get; set; }
    
    public int WarehouseId { get; set; }

    public int GoodId { get; set; }

    public int Quantity { get; set; }
}
