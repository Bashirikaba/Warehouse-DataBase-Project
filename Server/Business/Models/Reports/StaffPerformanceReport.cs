namespace Business.Models;

public class StaffPerformanceReport
{
    public virtual required string FullName { get; set; }

    public virtual required string Position { get; set; }

    public virtual required string WarehouseName { get; set; }

    public virtual int DocumentProcessed { get; set; }

    public virtual int UniqueGoodsHandled { get; set; }

    public virtual int TotalItemsProcessed { get; set; }

    public virtual decimal TotalValueProcessed { get; set; }

    public virtual DateTime LastWorkDate { get; set; }

    public override bool Equals(object obj)
    {
        var other = obj as StaffPerformanceReport;
        if (other == null) return false;
        return FullName == other.FullName && WarehouseName == other.WarehouseName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FullName, WarehouseName);
    }
}
