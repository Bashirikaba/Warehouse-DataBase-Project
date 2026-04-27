using Business.Enums;
using Business.Infrastructure;

namespace Business.Models;

public class WarehousePeriodReport : IReport
{
    public virtual required string WarehouseName { get; set; }

    public virtual required RouteType Route { get; set; }

    public virtual int InvoicesCount { get; set; }

    public virtual int UniqueGoods { get; set; }

    public virtual int TotalQuantity { get; set; }

    public virtual decimal TotalSum { get; set; }

    public virtual DateTime FirstDate { get; set; }

    public virtual DateTime LastDate { get; set; }

    public override bool Equals(object obj)
    {
        var other = obj as WarehousePeriodReport;
        if (other == null) return false;
        return WarehouseName == other.WarehouseName &&
                Route == other.Route &&
                FirstDate == other.FirstDate &&
                LastDate == other.LastDate;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(WarehouseName,
                                Route,
                                FirstDate,
                                LastDate);
    }
}

