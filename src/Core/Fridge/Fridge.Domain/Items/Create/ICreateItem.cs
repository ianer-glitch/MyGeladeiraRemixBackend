namespace Fridge.Domain.Items.Create;

public interface ICreateItem<TIn, TOut> 
    where TIn : ICreateItemIn 
    where TOut : ICreateItemOut
{
    public Task<TOut> ExecuteAsync(TIn request);
}