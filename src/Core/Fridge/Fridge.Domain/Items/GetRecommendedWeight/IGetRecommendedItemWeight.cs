using Fridge.Domain.Fridges.GetRecommendedItem;

namespace Fridge.Domain.Items.GetRecommendedWeight;

public interface IGetRecommendedItemWeight
{
    public Task<IGetRecommendedItemWeightOut> ExecuteAsync(IGetRecommendedItemWeightIn request);
}