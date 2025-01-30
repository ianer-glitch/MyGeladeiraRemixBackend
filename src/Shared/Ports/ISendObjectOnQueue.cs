namespace Ports;

public interface ISendObjectOnQueue
{
    public void Execute(object request, EQueue queue);
}