using Fridge.Domain.Fridges.RemoveItem;

namespace Fridge.Application.UseCases.Fridge.RemoveItems;

public class RemoveItemsFridgeIn : IRemoveItemsFridgeIn
{
    public Guid UserId { get; set; }
    public required  IEnumerable<Guid>  FridgeItemIds { get; set; }
}