using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;

namespace Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;

public class UpdateMultipleFridgeItemsQuantitiesIn : IUpdateMultipleFridgeItemsQuantitiesIn
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    
    public Guid UserId { get; set; }
}