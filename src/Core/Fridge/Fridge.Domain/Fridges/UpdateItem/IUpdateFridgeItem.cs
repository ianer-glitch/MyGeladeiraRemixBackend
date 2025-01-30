namespace Fridge.Domain.Fridges.UpdateItem;

public interface IUpdateFridgeItem
{
    public Task<IUpdateFridgeItemOut> ExecuteAsync(IUpdateFridgeItemIn request);
}