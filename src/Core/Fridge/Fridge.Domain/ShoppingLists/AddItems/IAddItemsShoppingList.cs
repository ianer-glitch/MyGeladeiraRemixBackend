namespace Fridge.Domain.ShoppingLists.AddItems;

public interface IAddItemsShoppingList
{
    public Task<IAddItemsShoppingListOut> ExecuteAsync(IAddItemsShoppingListIn request);
}