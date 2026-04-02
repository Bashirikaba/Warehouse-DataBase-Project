using API.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Mappings;

public class PositionMap : ClassMapping<Position>
{
    public PositionMap()
    {
        Table("positions");
        Id(x => x.Id, mapper => { mapper.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "positions_id_seq" })); mapper.Column("id"); });
        Property(x => x.Name, mapper => { mapper.Column("name"); mapper.Length(50); mapper.NotNullable(true); mapper.Unique(true); });
    }
}
