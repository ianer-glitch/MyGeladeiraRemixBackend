using Fridge.Domain.Fridges.RemoveItem;

namespace Fridge.Application.UseCases.Fridge.RemoveItems;

public class RemoveItemsFridgeOut : IRemoveItemsFridgeOut
{
    public bool Success { get; set; }
}