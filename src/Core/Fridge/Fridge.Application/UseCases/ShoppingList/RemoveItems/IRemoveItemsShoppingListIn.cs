using Fridge.Domain.ShoppingLists.RemoveItems;

namespace Fridge.Application.UseCases.ShoppingList.RemoveItems;

public class RemoveItemsShoppingListIn : IRemoveItemsShoppingListIn
{
    public Guid UserId { get; set; }
    public IEnumerable<Guid> FridgeItemIds { get; set; }
}