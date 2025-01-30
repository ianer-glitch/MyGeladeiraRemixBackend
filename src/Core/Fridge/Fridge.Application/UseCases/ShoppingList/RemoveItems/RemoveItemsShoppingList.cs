using Fridge.Domain.ShoppingLists.RemoveItems;

namespace Fridge.Application.UseCases.ShoppingList.RemoveItems;

public class RemoveItemsShoppingList : IRemoveItemsShoppingList
{
    private readonly IRepository<FridgeItemModel, FridgeContext> _fridgeItemRepository;
    private readonly IRepository<ShoppingListModel,FridgeContext> _shoppingListRepository;
    public RemoveItemsShoppingList(IRepository<FridgeItemModel, FridgeContext> fridgeItemRepository, IRepository<ShoppingListModel, FridgeContext> shoppingListRepository)
    {
        _fridgeItemRepository = fridgeItemRepository;
        _shoppingListRepository = shoppingListRepository;
    }
    public async Task<IRemoveItemsShoppingListOut> ExecuteAsync(IRemoveItemsShoppingListIn request)
    {
        try
        {
            var userShoppingList = _shoppingListRepository.Get(g=>g.UserId == request.UserId).FirstOrDefault();
            if (userShoppingList == null)
            {
                userShoppingList = await _shoppingListRepository.InsertAsync(new ShoppingListModel(request.UserId));
                if(await _shoppingListRepository.SaveChangesAsync() == 0)
                    throw new ArgumentNullException(nameof(userShoppingList));
            }
            
            await _fridgeItemRepository
                .Get(g=> request.FridgeItemIds.Contains(g.Id))
                .ForEachAsync(f=>f.ShoppingListId = null);

            return new RemoveItemsShoppingListOut
            {
                Success = await _fridgeItemRepository.SaveChangesAsync() > 0,
            };

        }
        catch (Exception e)
        {
            throw;
        }
    }
}