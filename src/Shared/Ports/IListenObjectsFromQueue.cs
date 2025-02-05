namespace Ports;

public interface IListenObjectsFromQueue
{
    public Task ExecuteAsync<TIn, TOut>(Func<TIn, Task<TOut>> functionToRun, CancellationToken cancelToken,
        EQueue queue);
}