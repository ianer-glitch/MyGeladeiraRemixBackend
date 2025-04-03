using Fridge.Application.UseCases.ShoppingList;
using Fridge.Application.UseCases.ShoppingList.AddItems;
using Fridge.Application.UseCases.ShoppingList.RemoveItems;
using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;
using Fridge.Domain.ShoppingLists.AddItems;
using Fridge.Domain.ShoppingLists.RemoveItems;
using Microsoft.Extensions.Logging;
using Ports.Expired;

namespace Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;

public class UpdateMultipleFridgeItemsQuantities : IUpdateMultipleFridgeItemsQuantities
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _fridgeItemRepository;
    private readonly IAddItemsShoppingList _addItemsShoppingList;
    private readonly IRemoveItemsShoppingList _removeItemsShoppingList;
    private readonly ISendObjectOnQueue _sendObjectOnQueue;
    private readonly ILogger<UpdateMultipleFridgeItemsQuantities> _logger;
    public UpdateMultipleFridgeItemsQuantities(IRepository<FridgeItemModel,FridgeContext> fridgeItemRepository, IAddItemsShoppingList addItemsShoppingList, IRemoveItemsShoppingList removeItemsShoppingList, ISendObjectOnQueue sendObjectOnQueue, ILogger<UpdateMultipleFridgeItemsQuantities> logger)
    {
        _fridgeItemRepository = fridgeItemRepository;
        _addItemsShoppingList = addItemsShoppingList;
        _removeItemsShoppingList = removeItemsShoppingList;
        _sendObjectOnQueue = sendObjectOnQueue;
        _logger = logger;
    }
    public async Task<IUpdateMultipleFridgeItemsQuantitiesOut> ExecuteAsync(IEnumerable<IUpdateMultipleFridgeItemsQuantitiesIn> request)
    {
        try
        {
            var items  =_fridgeItemRepository.Get(g => 
                                                                                request.Select(s => s.ItemId)
                                                                                       .Contains(g.Id));
            var itemNames = string.Join(",", items.Select(s => s.Name));
            _logger.LogInformation("Updating Itens : {itemNames}",itemNames);
            
            var relationalList=
                (from req in request
                    join ite in items on req.ItemId equals ite.Id
                    select new
                    {
                        req,ite 
                    }).ToList(); 
            
            relationalList.ForEach(f =>
            {
                f.ite.Quantity =f.req.Quantity;
                if (f.ite.IsExpired)
                {
                    _logger.LogInformation("Sending Expired Statistic : {Name}",f.ite.Name);
                    _sendObjectOnQueue.Execute(new CreateExpiredStatisticIn
                    {
                        ItemId = f.ite.Id,
                        UserId = request.Select(s => s.UserId).FirstOrDefault(),
                        ItemWeight = f.ite.Weight,
                    },EQueue.ExpiredStatistic);
                }

                f.ite.UpdateItemExpiration();
            });
            
            _fridgeItemRepository.UpdateRange(items);

            await _fridgeItemRepository.SaveChangesAsync();
            
            var listToAdd = relationalList.Where(w => w.ite.ShouldAddToShoppingList).Select(s => s.ite.Id);
            if (listToAdd.Any())
            {
               
                _logger.LogInformation("Adding Items to Shopping List : {listToAdd}",listToAdd);
                await _addItemsShoppingList.ExecuteAsync(new AddItemsShoppingListIn
                {
                    FridgeItemIds = listToAdd,
                    UserId = request.Select(s => s.UserId).FirstOrDefault()

                });
                
            }

            var listToRemove = relationalList.Where(w => !w.ite.ShouldAddToShoppingList).Select(s => s.ite.Id);
            if (listToRemove.Any())
            {
                _logger.LogInformation("Removing Items from Shopping List : {listToRemove}",listToRemove);
                await _removeItemsShoppingList.ExecuteAsync(new RemoveItemsShoppingListIn
                    {
                        FridgeItemIds = listToRemove,
                        UserId = request.Select(s => s.UserId).FirstOrDefault()
                });
            
            }
            
            
            
            return new UpdateMultipleFridgeItemsQuantitiesOut
            {
                Success = true,
            };
        }
        catch (Exception e)
        {
            _logger.LogError("Could not update item quantities {Message},{InnerException}",e.Message,e.InnerException);
            throw;
        }
    }
}