namespace API.Models;

public class Goods
{
    public virtual int Id { get; set; }
    
    public virtual required string Code { get; set; }
    
    public virtual required string NomenclatureNumber { get; set; }
    
    public virtual required string Name { get; set; }
    
    public virtual decimal Price { get; set; }
}
