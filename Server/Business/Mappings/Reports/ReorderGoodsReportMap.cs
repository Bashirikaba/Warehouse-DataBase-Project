using Business.Models;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings.Reports;

public class ReorderGoodsReportMap : ClassMapping<ReorderGoodsReport>
{
    public ReorderGoodsReportMap()
    {
        Mutable(false);
        Table("warehouse_period_map");
        ComposedId(map =>
        {
            map.Property(p => p.Name);
            map.Property(p => p.Code);
            map.Property(p => p.WarehouseName);
        });
        Property(x => x.Quantity, m =>
        {
            m.Column("quantity");
        });
        Property(x => x.SoldLast30Days, m =>
        {
            m.Column("sold_last_30_days");
        });
    }
}
