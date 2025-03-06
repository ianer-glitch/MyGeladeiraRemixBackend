namespace Fridge.Domain.Fridges.GetRecommendedItem;

public interface IGetRecommendedItems
{
    public Task<List<IGetRecommendedItemsOut>> ExecuteAsync(IGetRecommendedItemsIn request);
}