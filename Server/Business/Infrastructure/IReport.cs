namespace Business.Infrastructure;

public interface IReport
{
    public bool Equals(object obj);
    
    public int GetHashCode();
}
