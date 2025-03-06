using Fridge.Domain.Fridges.GetRecommendedItem;

namespace Fridge.Application.UseCases.Fridge.GetRecommendedItem;

public class GetRecommendedItemsOut : IGetRecommendedItemsOut
{
    public required string  Name { get; set; }
    public required string  Color { get; set; }
}