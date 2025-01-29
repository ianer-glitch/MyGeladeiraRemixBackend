using Fridge.Domain.ShoppingLists.GetItems;

namespace Fridge.Application.UseCases.ShoppingList.GetItems;

public class GetItemsShoppingListIn : IGetItemsShoppingListIn
{
    public Guid UserId { get; set; }
}