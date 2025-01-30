namespace Fridge.Domain.Items.Delete;

public interface IDeleteItem
{
    public Task<IDeleteItemOut> ExecuteAsync(IDeleteItemIn request);
}