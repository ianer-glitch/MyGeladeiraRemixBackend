namespace Fridge.Domain.ShoppingLists.GetItems;

public interface IGetItemsShoppingList
{
    public Task<IEnumerable<IGetItemsShoppingListOut>> ExecuteAsync(IGetItemsShoppingListIn request);
}