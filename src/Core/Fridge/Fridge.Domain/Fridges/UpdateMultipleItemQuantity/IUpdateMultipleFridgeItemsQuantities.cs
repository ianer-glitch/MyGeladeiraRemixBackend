namespace Fridge.Domain.Fridges.UpdateMultipleItemQuantity;

public interface IUpdateMultipleFridgeItemsQuantities
{
    public Task<IUpdateMultipleFridgeItemsQuantitiesOut> ExecuteAsync(IEnumerable<IUpdateMultipleFridgeItemsQuantitiesIn> request);
}