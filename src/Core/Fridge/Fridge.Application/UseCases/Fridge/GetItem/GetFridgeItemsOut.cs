using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Items.Get;

namespace Fridge.Application.UseCases.Fridge.GetItem;

public class GetFridgeItemsOut : IGetFridgeItemsOut
{
    public required string IconLink { get; set; }
    public required string Color { get; set; }
    public required string PercentageExpired { get; set; }
    public int Count { get; set; }
    public Guid ItemId { get; set; }
}