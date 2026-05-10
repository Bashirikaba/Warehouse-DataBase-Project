using Business.Enums;
using Business.Models;

namespace Business.Dto;

/// <summary>
/// Извещение о приходе/расходе
/// </summary>
public class InvoiceDto
{
    public int? Id { get; set; }

    public required Warehouse Warehouse { get; set; }

    public required Good Good { get; set; }

    public required string InvoiceNumber { get; set; }

    public DateTime Date { get; set; }

    public int RouteType { get; set; }

    public int Quantity { get; set; }

    public decimal Cost { get; set; }
}
