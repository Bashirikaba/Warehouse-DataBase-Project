using API.Models.Tables;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace API.Mappings.Tables;

public class GoodMap : ClassMapping<Good>
{
    public GoodMap()
    {
        Table("goods");
        Id(x => x.Id, m =>
        {
            m.Column("id");
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "goods_id_seq" }));
        });
        Property(x => x.Code, m =>
        {
            m.Column("code");
            m.Length(15); m.NotNullable(true); m.Unique(true);
        });
        Property(x => x.NomenclatureNumber, m =>
        {
            m.Column("nomenclature_number");
            m.Length(5); m.NotNullable(true);
            m.UniqueKey("goods_nomenclature_number_name_key");
        });
        Property(x => x.Name, m =>
        {
            m.Column("name");
            m.Length(200); m.NotNullable(true);
            m.UniqueKey("goods_nomenclature_number_name_key");
        });
        Property(x => x.Price, m =>
        {
            m.Column("price");
            m.NotNullable(true); m.Precision(9); m.Scale(2);
        });
    }
}
