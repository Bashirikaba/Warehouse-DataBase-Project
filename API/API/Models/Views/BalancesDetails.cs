namespace API.Models.Views;

/// <summary>
/// Детальная информация об остатках товара
/// </summary>
public class BalancesDetails
{
    public virtual int Id {get;set;}

    public virtual required string WarehouseName {get;set;}

    public virtual required string GoodsName {get;set;}

    public virtual required string GoodsCode {get;set;}

    public virtual int Quantity {get;set;}
}
