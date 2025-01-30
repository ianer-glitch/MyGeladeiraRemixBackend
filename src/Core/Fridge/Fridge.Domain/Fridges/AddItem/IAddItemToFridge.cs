namespace Fridge.Domain.Fridges.AddItem;

public interface IAddItemsToFridge
{
    public Task<IAddItemsToFridgeOut> ExecuteAsync(IAddItemsToFridgeIn request);
}