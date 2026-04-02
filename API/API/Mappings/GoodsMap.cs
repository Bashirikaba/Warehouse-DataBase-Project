using API.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Mappings;

public class GoodsMap : ClassMapping<Goods>
{
    public GoodsMap()
    {
        Table("goods");
        Id(x => x.Id, mapper => { mapper.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "goods_id_seq" })); mapper.Column("id"); });
        Property(x => x.Code, mapper => { mapper.Column("code"); mapper.Length(15); mapper.NotNullable(true); mapper.Unique(true); });
        Property(x => x.NomenclatureNumber, mapper => { mapper.Column("nomenclature_number"); mapper.Length(5); mapper.NotNullable(true); });
        Property(x => x.Name, mapper => { mapper.Column("name"); mapper.Length(200); mapper.NotNullable(true); });
        Property(x => x.Price, mapper => { mapper.Column("price"); mapper.NotNullable(true); mapper.Precision(9); mapper.Scale(2); });
    }
}
