using Fridge.Domain.Fridges.GetRecommendedItem;

namespace Fridge.Application.UseCases.Fridge.GetRecommendedItem;

public class GetRecommendedItemsIn : IGetRecommendedItemsIn
{
    public Guid UserId { get; set; }
}