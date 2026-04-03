using API.Models.Tables;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Mappings.Tables;

public class BalanceMap : ClassMapping<Balance>
{
    public BalanceMap()
    {
        Table("balances");
        Id(x => x.Id, m =>
        {
            m.Column("id");
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "balances_id_seq" }));
        });
        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("warehouse_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
            m.UniqueKey("unique_warehouse_goods");
        });
        ManyToOne(x => x.Good, m =>
        {
            m.Column("goods_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
            m.UniqueKey("unique_warehouse_goods");
        });
        Property(x => x.Quantity, m =>
        {
            m.Column(c =>
            {
                c.Name("quantity");
                c.Default(0);
            });
            m.NotNullable(true);
        });
        // TODO Уникальное ограничение (warehouse_id, goods_id) можно задать через Component или в конфигурации схемы.
    }
}