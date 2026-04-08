using Business.Enums;
using Business.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Business.Mappings;

public class InvoiceMap : ClassMapping<Invoice>
{
    public InvoiceMap()
    {
        Table("invoice");
        Id(x => x.Id, m =>
        {
            m.Column("id");
            m.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "invoice_id_seq" }));
        });
        Property(x => x.InvoiceNumber, m =>
        {
            m.Column("invoice_number");
            m.NotNullable(true);
            m.Unique(true);
            m.Length(5);
        });
        Property(x => x.Date, m =>
        {
            m.Column(c =>
            {
                c.Name("date");
                c.Default("CURRENT_DATE");
            });
            m.NotNullable(true);
        });
        Property(x => x.RouteType, m =>
        {
            m.Column("route");
            m.NotNullable(true);
            m.Type<EnumType<RouteType>>();
        });
        Property(x => x.Quantity, m =>
        {
            m.Column(c =>
            {
                c.Name("quantity");
                c.Check("quantity > 0");
            });
            m.Column("quantity");
            m.NotNullable(true);
        });
        Property(x => x.Cost, m =>
        {
            m.Column(c =>
            {
                c.Name("cost");
                c.Check("cost >= 0");
            });
            m.NotNullable(true);
            m.Precision(11);
            m.Scale(2);
        });
        ManyToOne(x => x.Warehouse, m =>
        {
            m.Column("warehouse_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
        ManyToOne(x => x.Good, m =>
        {
            m.Column("goods_id");
            m.NotNullable(true);
            m.Cascade(Cascade.None);
        });
    }
}