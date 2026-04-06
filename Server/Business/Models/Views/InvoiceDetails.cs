namespace Business.Models;

/// <summary>
/// Детальная информация об извещении
/// </summary>
public class InvoiceDetails
{
    public virtual required int Id { get; set; }

    public virtual int InvoiceNumber { get; set; }

    public virtual DateTime Date { get; set; }

    public virtual RouteType Route { get; set; }

    public virtual required string WarehouseName { get; set; }

    public virtual required string GoodsName { get; set; }

    public virtual required string GoodsCode { get; set; }

    public virtual int Quantity { get; set; }

    public virtual decimal Cost { get; set; }

    public virtual decimal TotalAmount { get; set; }
}
