

using Fridge.Application.UseCases.ShoppingList;
using Fridge.Application.UseCases.ShoppingList.AddItems;
using Fridge.Application.UseCases.ShoppingList.RemoveItems;
using Fridge.Domain.ShoppingLists.AddItems;
using Fridge.Domain.ShoppingLists.RemoveItems;
using Ports.Expired;

namespace Fridge.Application.UseCases.Fridge.UpdateItem;

public class UpdateFridgeItem : IUpdateFridgeItem
{
    private readonly IRepository<FridgeItem,FridgeContext> _repository;
    private readonly IAddItemsShoppingList _addItemsShoppingList;
    private readonly IRemoveItemsShoppingList _removeItemsShoppingList;
    private readonly ISendObjectOnQueue _sendObjectOnQueue;
    public UpdateFridgeItem(IRepository<FridgeItem, FridgeContext> repository, IAddItemsShoppingList addItemsShoppingList, IRemoveItemsShoppingList removeItemsShoppingList, ISendObjectOnQueue sendObjectOnQueue)
    {
        _repository = repository;
        _addItemsShoppingList = addItemsShoppingList;
        _removeItemsShoppingList = removeItemsShoppingList;
        _sendObjectOnQueue = sendObjectOnQueue;
    }
    public async Task<IUpdateFridgeItemOut> ExecuteAsync(IUpdateFridgeItemIn request)
    {
        try
        {
            var currenctItem =  await _repository.Get(g => g.Id == request.ItemId).FirstOrDefaultAsync();
            await HandleExpiredItem(currenctItem, request.UserId);
            
            if(currenctItem == null)
                throw new ArgumentNullException(nameof(currenctItem));    
            
            currenctItem.Modified = DateTime.UtcNow;
            currenctItem.Quantity = request.Quantity;
            currenctItem.MinimunQuantity = request.MinimunQuantity;
            currenctItem.Expiration = request.Expiration.ToUniversalTime();
            
            await AddOrRemoveFromShoppingList(currenctItem,request.UserId);
            
            _repository.Update(currenctItem);
            
            return new UpdateFridgeItemOut()
            {
                Success = await _repository.SaveChangesAsync() > 0,
            };

        }
        catch (Exception e)
        {
            throw;
        }

    }
    private async Task AddOrRemoveFromShoppingList(FridgeItem currenctItem , Guid userId)
    {
        if (currenctItem.ShouldAddToShoppingList)
        {
            await _addItemsShoppingList.ExecuteAsync(new AddItemsShoppingListIn
            {
                FridgeItemIds = Enumerable.Empty<Guid>().Append(currenctItem.Id),
                UserId = userId
            });
        }
        else
        {
            await _removeItemsShoppingList.ExecuteAsync(new RemoveItemsShoppingListIn
            {
                FridgeItemIds = Enumerable.Empty<Guid>().Append(currenctItem.Id),
                UserId = userId
            });
        }
    }


    private async Task HandleExpiredItem(FridgeItem currenctItem, Guid userId)
    {
        if (currenctItem.IsExpired)
        {
            _sendObjectOnQueue.Execute(new CreateExpiredStatisticIn
            {
                ItemId = currenctItem.Id,
                UserId = userId,
                ItemWeight = currenctItem.Weight,
            },EQueue.ExpiredStatistic);                    

        }
    }
}