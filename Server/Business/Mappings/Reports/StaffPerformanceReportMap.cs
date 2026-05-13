using Business.Models;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings.Reports;

public class StaffPerformanceReportMap : ClassMapping<StaffPerformanceReport>
{
    public StaffPerformanceReportMap()
    {
        Mutable(false);
        Table("staff_performance_report");
        ComposedId(map =>
        {
            map.Property(p => p.FullName, m => m.Column("full_name"));
            map.Property(p => p.WarehouseName, m => m.Column("warehouse_name"));
        });
        Property(x => x.Position, m =>
        {
            m.Column("position");
        });
        Property(x => x.DocumentsProcessed, m =>
        {
            m.Column("documents_processed");
        });
        Property(x => x.UniqueGoodsHandled, m =>
        {
            m.Column("unique_goods_handled");
        });
        Property(x => x.TotalItemsProcessed, m =>
        {
            m.Column("total_items_processed");
        });
        Property(x => x.TotalValueProcessed, m =>
        {
            m.Column("total_value_processed");
        });
        Property(x => x.LastWorkDate, m =>
        {
            m.Column("last_work_date");
        });
    }
}
