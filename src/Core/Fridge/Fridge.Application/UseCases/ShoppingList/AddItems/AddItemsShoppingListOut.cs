using Fridge.Domain.ShoppingLists.AddItems;

namespace Fridge.Application.UseCases.ShoppingList.AddItems;

public class AddItemsShoppingListOut : IAddItemsShoppingListOut
{
    public bool Success { get; set; }
}