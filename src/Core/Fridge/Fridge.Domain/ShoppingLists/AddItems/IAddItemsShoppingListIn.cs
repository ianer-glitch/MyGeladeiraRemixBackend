namespace Fridge.Domain.ShoppingLists.AddItems;

public interface IAddItemsShoppingListIn
{
    public Guid UserId { get; set; }
    public  IEnumerable<Guid> FridgeItemIds { get; set; }
}