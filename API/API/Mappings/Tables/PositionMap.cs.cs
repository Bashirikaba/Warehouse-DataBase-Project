using API.Models.Tables;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Mappings.Tables;

public class PositionMap : ClassMapping<Position>
{
    public PositionMap()
    {
        Table("positions");
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "positions_id_seq" }));
            m.Column("id");
        });
        Property(x => x.Name, m =>
        {
            m.Column("name");
            m.Length(50); m.NotNullable(true); m.Unique(true);
        });
    }
}
