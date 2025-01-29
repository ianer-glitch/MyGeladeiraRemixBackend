namespace Fridge.Domain.Fridges.RemoveItem;

public interface IRemoveItemsFridge
{
    public Task<IRemoveItemsFridgeOut> ExecuteAsync(IRemoveItemsFridgeIn request);
}