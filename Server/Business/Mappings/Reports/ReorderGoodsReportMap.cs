using Business.Models;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings.Reports;

public class ReorderGoodsReportMap : ClassMapping<ReorderGoodsReport>
{
    public ReorderGoodsReportMap()
    {
        Mutable(false);
        Table("reorder_goods_report");
        ComposedId(map =>
        {
            map.Property(p => p.Name, m => m.Column("name"));
            map.Property(p => p.Code, m => m.Column("code"));
            map.Property(p => p.WarehouseName, m => m.Column("warehouse_name"));
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
