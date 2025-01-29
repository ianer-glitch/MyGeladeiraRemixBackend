using Fridge.Domain.Fridges.UpdateItem;

namespace Fridge.Application.UseCases.Fridge.UpdateItem;

public class UpdateFridgeItemIn : IUpdateFridgeItemIn
{
    public DateTime Expiration { get; set; }
    public int MinimunQuantity { get; set; }
    public int Quantity { get; set; }
    public Guid ItemId { get; set; }
}