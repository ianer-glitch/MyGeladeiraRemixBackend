using Fridge.Domain.ShoppingLists.GetItems;

namespace Fridge.Application.UseCases.ShoppingList.GetItems;

public class GetItemsShoppingList : IGetItemsShoppingList
{
    private readonly IRepository<ShoppingListModel,FridgeContext> _shoppingListRepository;
    private readonly IRepository<FridgeItem, FridgeContext> _fridgeItemRepository;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;

    public GetItemsShoppingList(
        IRepository<ShoppingListModel, FridgeContext> shoppingListRepository,
        IRepository<FridgeItem, FridgeContext> fridgeItemRepository, IFileAdapter<IFileAdapterResult> fileAdapter)
    {
        _shoppingListRepository = shoppingListRepository;
        _fridgeItemRepository = fridgeItemRepository;
        _fileAdapter = fileAdapter;
    }
    public async  Task<IEnumerable<IGetItemsShoppingListOut>> ExecuteAsync(IGetItemsShoppingListIn request)
    {
        try
        {
            var userShoppingList = await _shoppingListRepository.Get(f=>f.UserId == request.UserId)
                                                                .FirstOrDefaultAsync();
            if(userShoppingList == null)
               return Enumerable.Empty<GetItemsShoppingListOut>();
            
            var items = _fridgeItemRepository.Get(g => g.ShoppingListId == userShoppingList.Id && g.IsActive);

            var itemsOut = items.Select(s => new GetItemsShoppingListOut
            {
                ItemId = s.Id,
                ItemColor = s.Color,
                ItemName = s.Name,
                IconName = s.IconName,
            }).ToList();

            itemsOut.ForEach(async f =>
            {
                if (string.IsNullOrEmpty(f.IconName)) return;
                
                var file = await _fileAdapter.GetFileAsync(f.IconName!);
                f.IconLink = file.Link;

            });

            return itemsOut;



        }
        catch (Exception e)
        {
            throw;
        }
    }
}