namespace API.Models.Views;

/// <summary>
/// Остатки товара на складе
/// </summary>
public class WarehouseTotals
{
    public virtual int Id {get;set;}
    
    public virtual required string WarehouseName {get;set;}   
    
    public virtual int UniqueGoodsCount {get;set;}   
    
    public virtual int TotalItems {get;set;}   
}
