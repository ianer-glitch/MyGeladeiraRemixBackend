using Fridge.Domain.ShoppingLists.GetItems;

namespace Fridge.Application.UseCases.ShoppingList.GetItems;

public class GetItemsShoppingListOut : IGetItemsShoppingListOut
{
    public required string ItemName { get; set; }
    public required string ItemColor { get; set; }
    public Guid ItemId { get; set; }
}