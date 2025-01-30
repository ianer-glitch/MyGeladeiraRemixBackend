using Fridge.Domain.ShoppingLists.GetItems;

namespace Fridge.Application.UseCases.ShoppingList.GetItems;

public class GetItemsShoppingList : IGetItemsShoppingList
{
    private readonly IRepository<ShoppingListModel,FridgeContext> _shoppingListRepository;
    private readonly IRepository<FridgeItem, FridgeContext> _fridgeItemRepository;
    public GetItemsShoppingList(IRepository<ShoppingListModel, FridgeContext> shoppingListRepository,
        IRepository<FridgeItem, FridgeContext> fridgeItemRepository)
    {
        _shoppingListRepository = shoppingListRepository;
        _fridgeItemRepository = fridgeItemRepository;
    }
    public async  Task<IEnumerable<IGetItemsShoppingListOut>> ExecuteAsync(IGetItemsShoppingListIn request)
    {
        try
        {
            var userShoppingList = await _shoppingListRepository.Get(f=>f.UserId == request.UserId)
                                                                .FirstOrDefaultAsync();
            if(userShoppingList == null)
                throw new ArgumentNullException(nameof(userShoppingList));
            
            var items = _fridgeItemRepository.Get(g => g.ShoppingListId == userShoppingList.Id);

            return items.Select(s => new GetItemsShoppingListOut
            {
                ItemId = s.Id,
                ItemColor = s.Color,
                ItemName = s.Name,
            });
        }
        catch (Exception e)
        {
            throw;
        }
    }
}