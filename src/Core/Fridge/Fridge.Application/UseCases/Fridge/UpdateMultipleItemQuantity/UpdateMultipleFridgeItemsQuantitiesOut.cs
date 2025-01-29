using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;

namespace Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;

public class UpdateMultipleFridgeItemsQuantitiesOut : IUpdateMultipleFridgeItemsQuantitiesOut
{
    public bool Success { get; set; }
}