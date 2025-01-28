namespace Fridge.Domain.Fridges.GetItem;

public interface IGetFridgeItems
{
    public Task<IEnumerable<IGetFridgeItemsOut>>ExecuteAsync(IGetFridgeItemsIn request);
}