namespace Fridge.Domain.Items.Create;

public interface ICreateItem
{
    public Task<ICreateItemOut> ExecuteAsync(ICreateItemIn request);
}