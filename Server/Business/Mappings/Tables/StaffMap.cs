using Business.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Business.Mappings;

public class StaffMap : ClassMapping<Staff>
{
    public StaffMap()
    {
        Table("staff");
        Id(x => x.Id, m =>
        {
            m.Column("id");
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "staff_id_seq" }));
        });
        Property(x => x.FullName, m =>
        {
            m.Column("full_name");
            m.NotNullable(true);
            m.Length(50);
        });
        Property(x => x.TIN, m =>
        {
            m.Column("tin");
            m.NotNullable(true);
            m.Unique(true);
            m.Length(12);
        });
        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("warehouse_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
        ManyToOne(x => x.Position, m =>
        {
            m.Column("position_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
    }
}