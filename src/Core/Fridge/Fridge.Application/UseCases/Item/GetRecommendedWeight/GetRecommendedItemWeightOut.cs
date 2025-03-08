using Fridge.Domain.Items.GetRecommendedWeight;

namespace Fridge.Application.UseCases.Item.GetRecommendedWeight;

public class GetRecommendedItemWeightOut : IGetRecommendedItemWeightOut
{
    public double Weight { get; set; }
}