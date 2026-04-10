using Business.Infrastructure;

namespace Business.Models;

public class ReorderGoodsReport : IReport
{
    public virtual required string Name { get; set; }
    
    public virtual required string Code { get; set; }
    
    public virtual required string WarehouseName { get; set; }
    
    public virtual int Quantity { get; set; }
    
    public virtual int SoldLast30Days { get; set; }

    public override bool Equals(object obj)
    {
        var other = obj as ReorderGoodsReport;
        if (other == null) return false;
        return Name == other.Name && Code == other.Code && WarehouseName == other.WarehouseName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Code, WarehouseName);
    }
}
