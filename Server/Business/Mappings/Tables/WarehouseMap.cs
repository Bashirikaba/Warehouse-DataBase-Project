using Business.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings;

public class WarehouseMap : ClassMapping<Warehouse>
{
    public WarehouseMap()
    {
        Table("warehouses");
        Id(x => x.Id, m =>
        {
            m.Column("id");
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "warehouses_id_seq" }));
        });
        Property(x => x.Name, m =>
        {
            m.Column("name");
            m.NotNullable(true);
            m.Unique(true);
            m.Length(50);
        });
        ManyToOne(x => x.Manager, m =>
        {
            m.Column("manager_id");
            m.NotNullable(false);
            m.Cascade(Cascade.None);
            m.NotFound(NotFoundMode.Ignore);
        });
    }
}