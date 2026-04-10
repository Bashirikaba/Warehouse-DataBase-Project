namespace Business.Dto;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class BalanceDto
{
    public int? Id { get; set; }
    
    public required string WarehouseName { get; set; }

    public required string GoodNomenclatureNumber { get; set; }

    public int Quantity { get; set; }
}
