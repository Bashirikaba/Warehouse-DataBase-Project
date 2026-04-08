using Business.Enums;

namespace Business.Dto;

/// <summary>
/// Извещение о приходе/расходе
/// </summary>
public class InvoiceDto
{
    public int? Id { get; set; }

    public int WarehouseId { get; set; }

    public int GoodId { get; set; }

    public required string InvoiceNumber { get; set; }

    public DateTime Date { get; set; }

    public RouteType RouteType { get; set; }

    public int Quantity { get; set; }

    public decimal Cost { get; set; }

}
