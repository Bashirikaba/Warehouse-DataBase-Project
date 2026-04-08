namespace Business.Dto;
!!!ПОДСТАВИТЬ СЮДА ОТЧЕТЫ вместо этих вьюшек
/// <summary>
/// Детальная информация об остатках товара
/// </summary>
public class BalancesDetailsDto
{
    public virtual int Id {get;set;}

    public virtual required string WarehouseName {get;set;}

    public virtual required string GoodsName {get;set;}

    public virtual required string GoodsCode {get;set;}

    public virtual int Quantity {get;set;}
}
