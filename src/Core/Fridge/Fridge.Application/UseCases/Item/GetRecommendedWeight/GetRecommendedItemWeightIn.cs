using Fridge.Domain.Items.GetRecommendedWeight;

namespace Fridge.Application.UseCases.Item.GetRecommendedWeight;

public class GetRecommendedItemWeightIn : IGetRecommendedItemWeightIn
{
    public string Name { get; set; }
}