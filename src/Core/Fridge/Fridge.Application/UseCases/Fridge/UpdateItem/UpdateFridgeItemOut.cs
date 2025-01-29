using Fridge.Domain.Fridges.UpdateItem;

namespace Fridge.Application.UseCases.Fridge.UpdateItem;

public class UpdateFridgeItemOut : IUpdateFridgeItemOut
{
    public bool Success { get; set; }
}