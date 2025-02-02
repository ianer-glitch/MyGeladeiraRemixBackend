namespace Ports;

public interface IListenObjectsFromQueue
{
    public void Execute<TIn,TOut>(Func<TIn,TOut> functionToRun, CancellationToken cancelToken, EQueue queue);
}