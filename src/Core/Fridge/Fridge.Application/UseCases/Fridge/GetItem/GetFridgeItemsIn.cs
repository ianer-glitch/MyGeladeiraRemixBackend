using Fridge.Domain.Fridges.GetItem;

namespace Fridge.Application.UseCases.Fridge.GetItem;

public class GetFridgeItemsIn: IGetFridgeItemsIn
{
    public Guid UserId { get; set; }
}