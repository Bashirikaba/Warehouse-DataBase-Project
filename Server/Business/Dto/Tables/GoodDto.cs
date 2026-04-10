namespace Business.Dto;

/// <summary>
/// Информация о товаре
/// </summary>
public class GoodDto
{
    public int? Id { get; set; }
    
    public required string Code { get; set; }
    
    public required string NomenclatureNumber { get; set; }
    
    public required string Name { get; set; }
    
    public decimal Price { get; set; }
}
