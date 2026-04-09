using Business.Models;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings.Reports;

public class WarehousePeriodReportMap : ClassMapping<WarehousePeriodReport>
{
    public WarehousePeriodReportMap()
    {
        Mutable(false);
        Table("warehouse_period_map");
        ComposedId(map =>
        {
            map.Property(p => p.WarehouseName);
            map.Property(p => p.Route);
            map.Property(p => p.FirstDate);
            map.Property(p => p.LastDate);
        });
        Property(x => x.InvoicesCount, m =>
        {
            m.Column("invoices_count");
        });
        Property(x => x.UniqueGoods, m =>
        {
            m.Column("unique_goods");
        });
        Property(x => x.TotalQuantity, m =>
        {
            m.Column("total_quantity");
        });
        Property(x => x.TotalSum, m =>
        {
            m.Column("total_sum");
        });
    }
}
