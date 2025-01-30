namespace Fridge.Domain.ShoppingLists.GetItems;

public interface IGetItemsShoppingListOut
{
    public string ItemName { get; set; }
    public string ItemColor { get; set; }
    public Guid ItemId { get; set; }
}