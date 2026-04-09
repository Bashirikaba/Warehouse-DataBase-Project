using Business.Enums;
using Business.Infrastructure;

namespace Business.Models;

/// <summary>
/// Извещение о приходе/расходе
/// </summary>
public class Invoice : IEntity
{
    public virtual int Id { get; set; }

    public virtual required Warehouse Warehouse { get; set; }

    public virtual required Good Good { get; set; }

    public virtual required string InvoiceNumber { get; set; }

    public virtual DateTime Date { get; set; }

    public virtual RouteType RouteType { get; set; }

    public virtual int Quantity { get; set; }

    public virtual decimal Cost { get; set; }

}
