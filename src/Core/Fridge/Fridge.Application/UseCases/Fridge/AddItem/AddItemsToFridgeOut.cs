using Fridge.Domain.Fridges.AddItem;

namespace Fridge.Application.UseCases.Fridge.AddItem;

public class AddItemsToFridgeOut : IAddItemsToFridgeOut
{
    public bool Success { get; set; }
}