using Fridge.Domain.ShoppingLists.RemoveItems;

namespace Fridge.Application.UseCases.ShoppingList.RemoveItems;

public class RemoveItemsShoppingListOut : IRemoveItemsShoppingListOut
{
    public bool Success { get; set; }
}