namespace Identity.Domain.Ports;

public interface IConnectionHelper
{
    public T GetUserConnection<T>() where T: class; 
    
    public T GetPlanConnection<T>() where T: class;
}