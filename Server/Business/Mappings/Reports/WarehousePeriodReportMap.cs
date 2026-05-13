using Business.Models;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings.Reports;

public class WarehousePeriodReportMap : ClassMapping<WarehousePeriodReport>
{
    public WarehousePeriodReportMap()
    {
        Mutable(false);
        Table("warehouse_period_report");
        ComposedId(map =>
        {
            map.Property(p => p.WarehouseName, m => m.Column("warehouse_name"));
            map.Property(p => p.Route, m => m.Column("route"));
            map.Property(p => p.FirstDate, m => m.Column("first_date"));
            map.Property(p => p.LastDate, m => m.Column("last_date"));
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
