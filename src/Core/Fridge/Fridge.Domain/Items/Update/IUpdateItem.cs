namespace Fridge.Domain.Items.Update;

public interface IUpdateItem
{
    public Task<IUpdateItemOut> ExecuteAsync(IUpdateItemIn request);
}