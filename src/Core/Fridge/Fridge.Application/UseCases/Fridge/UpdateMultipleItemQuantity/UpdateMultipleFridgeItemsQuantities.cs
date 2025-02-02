using Fridge.Application.UseCases.ShoppingList;
using Fridge.Application.UseCases.ShoppingList.AddItems;
using Fridge.Application.UseCases.ShoppingList.RemoveItems;
using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;
using Fridge.Domain.ShoppingLists.AddItems;
using Fridge.Domain.ShoppingLists.RemoveItems;

namespace Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;

public class UpdateMultipleFridgeItemsQuantities : IUpdateMultipleFridgeItemsQuantities
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _fridgeItemRepository;
    private readonly IAddItemsShoppingList _addItemsShoppingList;
    private readonly IRemoveItemsShoppingList _removeItemsShoppingList;
    private readonly ISendObjectOnQueue _sendObjectOnQueue;
    public UpdateMultipleFridgeItemsQuantities(IRepository<FridgeItemModel,FridgeContext> fridgeItemRepository, IAddItemsShoppingList addItemsShoppingList, IRemoveItemsShoppingList removeItemsShoppingList, ISendObjectOnQueue sendObjectOnQueue)
    {
        _fridgeItemRepository = fridgeItemRepository;
        _addItemsShoppingList = addItemsShoppingList;
        _removeItemsShoppingList = removeItemsShoppingList;
        _sendObjectOnQueue = sendObjectOnQueue;
    }
    public async Task<IUpdateMultipleFridgeItemsQuantitiesOut> ExecuteAsync(IEnumerable<IUpdateMultipleFridgeItemsQuantitiesIn> request)
    {
        try
        {
            var items  =_fridgeItemRepository.Get(g => 
                                                                                request.Select(s => s.ItemId)
                                                                                       .Contains(g.Id));
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
                    // if(f.ite.IsExpired)
                        _sendObjectOnQueue.Execute("Item Quantity Change",EQueue.ExpiredStatustic);
                });

            
            var listToAdd = relationalList.Where(w => w.ite.ShouldAddToShoppingList).Select(s => s.ite.Id);
            if (listToAdd.Any())
            {
                await _addItemsShoppingList.ExecuteAsync(new AddItemsShoppingListIn
                {
                    FridgeItemIds = listToAdd,
                    UserId = request.Select(s => s.UserId).FirstOrDefault()

                });
                
            }

            var listToRemove = relationalList.Where(w => !w.ite.ShouldAddToShoppingList).Select(s => s.ite.Id);
            if (listToRemove.Any())
            {
                await _removeItemsShoppingList.ExecuteAsync(new RemoveItemsShoppingListIn
                    {
                        FridgeItemIds = listToRemove,
                        UserId = request.Select(s => s.UserId).FirstOrDefault()
                });
            
            }
            _fridgeItemRepository.UpdateRange(items);
            
            return new UpdateMultipleFridgeItemsQuantitiesOut
            {
                Success =await  _fridgeItemRepository.SaveChangesAsync() > 0,
            };
        }
        catch (Exception e)
        {
            throw;
        }
    }
}