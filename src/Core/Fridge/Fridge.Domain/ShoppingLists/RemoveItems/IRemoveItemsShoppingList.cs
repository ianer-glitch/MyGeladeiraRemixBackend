namespace Fridge.Domain.ShoppingLists.RemoveItems;

public interface IRemoveItemsShoppingList
{
    public Task<IRemoveItemsShoppingListOut> ExecuteAsync(IRemoveItemsShoppingListIn request);
}