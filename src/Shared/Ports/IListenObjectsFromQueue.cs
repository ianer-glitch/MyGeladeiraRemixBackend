namespace Ports;

public interface IListenObjectsFromQueue
{
    public void Execute<T>(Action<T> functionToRun, CancellationToken cancelToken, EQueue queue);
}