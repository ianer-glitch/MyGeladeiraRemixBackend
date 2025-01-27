using Fridge.Domain.Fridges.AddItem;

namespace Fridge.Application.UseCases.Fridge.AddItem;

public class AddItemsToFridgeIn : IAddItemsToFridgeIn
{
    public Guid UserId { get; set; }
    public required IEnumerable<Guid> ItemIds { get; set; }
}