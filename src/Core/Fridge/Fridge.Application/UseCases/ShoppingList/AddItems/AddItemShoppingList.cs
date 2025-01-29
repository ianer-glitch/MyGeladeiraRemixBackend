using Fridge.Domain.ShoppingLists.AddItems;

namespace Fridge.Application.UseCases.ShoppingList.AddItems;

public class AddItemShoppingList : IAddItemsShoppingList
{
    private readonly IRepository<ShoppingListModel,FridgeContext> _shoppingListRepository;
    private readonly IRepository<FridgeItemModel,FridgeContext> _fridgeItemRepository;
    public AddItemShoppingList(IRepository<ShoppingListModel,FridgeContext> shoppingListRepository, IRepository<FridgeItemModel, FridgeContext> fridgeItemRepository)
    {
        _shoppingListRepository = shoppingListRepository;
        _fridgeItemRepository = fridgeItemRepository;
    }
    public async Task<IAddItemsShoppingListOut> ExecuteAsync(IAddItemsShoppingListIn request)
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
                .ForEachAsync(f=>f.ShoppingListId = userShoppingList.Id);

            return new AddItemsShoppingListOut
            {
                Success = await _fridgeItemRepository.SaveChangesAsync() > 0,
            };
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}