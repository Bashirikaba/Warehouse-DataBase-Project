namespace Business.Models;

/// <summary>
/// Движение товаров
/// </summary>
public class GoodsTurnover
{
    public virtual int Id {get;set;}
    
    public virtual required string GoodsName {get;set;}
    
    public virtual required string Code {get;set;}
    
    public virtual int DocumentsCount {get;set;}
    
    public virtual int TotalReceived {get;set;}
    
    public virtual int TotalSold {get;set;}
    
    public virtual int ReceivedSum {get;set;}
    
    public virtual int SoldSum {get;set;}
}
