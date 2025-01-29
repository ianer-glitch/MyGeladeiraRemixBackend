using Fridge.Domain.ShoppingLists.AddItems;

namespace Fridge.Application.UseCases.ShoppingList.AddItems;

public class AddItemsShoppingListIn : IAddItemsShoppingListIn
{
    public Guid UserId { get; set; }
    public required IEnumerable<Guid> FridgeItemIds { get; set; } 
}