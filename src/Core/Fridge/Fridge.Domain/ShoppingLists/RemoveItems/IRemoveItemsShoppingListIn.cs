namespace Fridge.Domain.ShoppingLists.RemoveItems;

public interface IRemoveItemsShoppingListIn
{
    public Guid UserId { get; set; }
    public  IEnumerable<Guid> FridgeItemIds { get; set; }
}