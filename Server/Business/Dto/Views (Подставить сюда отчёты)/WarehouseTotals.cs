namespace Business.Dto;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class WarehouseTotalsDto
{
    public virtual int Id {get;set;}
    
    public virtual required string WarehouseName {get;set;}   
    
    public virtual int UniqueGoodsCount {get;set;}   
    
    public virtual int TotalItems {get;set;}   
}
