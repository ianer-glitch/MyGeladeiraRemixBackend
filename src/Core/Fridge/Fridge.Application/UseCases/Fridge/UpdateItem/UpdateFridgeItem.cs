

using Fridge.Application.UseCases.ShoppingList;
using Fridge.Application.UseCases.ShoppingList.AddItems;
using Fridge.Application.UseCases.ShoppingList.RemoveItems;
using Fridge.Domain.ShoppingLists.AddItems;
using Fridge.Domain.ShoppingLists.RemoveItems;
using Microsoft.Extensions.Logging;
using Ports.Expired;

namespace Fridge.Application.UseCases.Fridge.UpdateItem;

public class UpdateFridgeItem : IUpdateFridgeItem
{
    private readonly IRepository<FridgeItem,FridgeContext> _repository;
    private readonly IAddItemsShoppingList _addItemsShoppingList;
    private readonly IRemoveItemsShoppingList _removeItemsShoppingList;
    private readonly ISendObjectOnQueue _sendObjectOnQueue;
    private readonly ILogger<UpdateFridgeItem> _logger; 
    public UpdateFridgeItem(IRepository<FridgeItem, FridgeContext> repository, IAddItemsShoppingList addItemsShoppingList, IRemoveItemsShoppingList removeItemsShoppingList, ISendObjectOnQueue sendObjectOnQueue, ILogger<UpdateFridgeItem> logger)
    {
        _repository = repository;
        _addItemsShoppingList = addItemsShoppingList;
        _removeItemsShoppingList = removeItemsShoppingList;
        _sendObjectOnQueue = sendObjectOnQueue;
        _logger = logger;
    }
    public async Task<IUpdateFridgeItemOut> ExecuteAsync(IUpdateFridgeItemIn request)
    {
        try
        {
            var currenctItem =  await _repository.Get(g => g.Id == request.ItemId).FirstOrDefaultAsync();
            _logger.LogInformation("Updating Item {Name}",currenctItem?.Name);
            await HandleExpiredItem(currenctItem, request.UserId);
            
            if(currenctItem == null)
                throw new ArgumentNullException(nameof(currenctItem));    
            
            currenctItem.Modified = DateTime.UtcNow;
            currenctItem.Quantity = request.Quantity;
            currenctItem.MinimunQuantity = request.MinimunQuantity;
            currenctItem.Expiration = request.Expiration.ToUniversalTime();
            currenctItem.SetTimeToExpire(request.Expiration.ToUniversalTime());
            
            await AddOrRemoveFromShoppingList(currenctItem,request.UserId);
            
            _repository.Update(currenctItem);
            
            return new UpdateFridgeItemOut()
            {
                Success = await _repository.SaveChangesAsync() > 0,
            };

        }
        catch (Exception e)
        {
            _logger.LogError("Could not update item {Message},{InnerException}",e.Message,e.InnerException);
            throw;
        }

    }
    private async Task AddOrRemoveFromShoppingList(FridgeItem currenctItem , Guid userId)
    {
        if (currenctItem.ShouldAddToShoppingList)
        {
            _logger.LogInformation("Adding to Shopping List {Name}",currenctItem?.Name);
            await _addItemsShoppingList.ExecuteAsync(new AddItemsShoppingListIn
            {
                FridgeItemIds = Enumerable.Empty<Guid>().Append(currenctItem.Id),
                UserId = userId
            });
        }
        else
        {
            _logger.LogInformation("Removinf from  Shopping List {Name}",currenctItem?.Name);
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
            _logger.LogInformation("Sending expired statistic from item  {Name}",currenctItem?.Name);
            _sendObjectOnQueue.Execute(new CreateExpiredStatisticIn
            {
                ItemId = currenctItem.Id,
                UserId = userId,
                ItemWeight = currenctItem.Weight,
            },EQueue.ExpiredStatistic);                    

        }
    }
}