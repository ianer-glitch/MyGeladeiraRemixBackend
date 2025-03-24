using Fridge.Domain.Fridges;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Infrastructure;
using Microsoft.Extensions.Logging;
using Ports;

namespace Fridge.Application.UseCases.Fridge.AddItem;

public class AddItemsToFridge : IAddItemsToFridge
{
    private readonly IRepository<FridgeItem,FridgeContext> _rFridgeItem;
    private readonly IRepository<Domain.Fridges.Fridge,FridgeContext> _rFridge;
    private readonly IRepository<Domain.Items.Item,FridgeContext> _rItem;
    private readonly ILogger<AddItemsToFridge> _logger;

    public AddItemsToFridge(IRepository<FridgeItem, FridgeContext> rFridgeItem,
        IRepository<Domain.Fridges.Fridge, FridgeContext> rFridge,
        IRepository<Domain.Items.Item, FridgeContext> rItem, ILogger<AddItemsToFridge> logger)
    {
        _rFridgeItem = rFridgeItem;
        _rFridge = rFridge;
        _rItem = rItem;
        _logger = logger;
    }
    
    public async Task<IAddItemsToFridgeOut> ExecuteAsync(IAddItemsToFridgeIn request)
    {
        try
        {
            var itemIds = string.Join(',', request.ItemIds);    
            _logger.LogInformation("Adding items {itemIds} to fridge.",itemIds);
            var userFridge =  _rFridge.Get(g=>g.UserId == request.UserId).FirstOrDefault();
            if (userFridge == null)
            {
                userFridge = new Domain.Fridges.Fridge(request.UserId,request.UserId);
                await _rFridge.InsertAsync(userFridge);
                if(await _rFridge.SaveChangesAsync() == 0)
                    throw new  Exception("Could not create Fridge for user" + request.UserId);
            }
            var itemsAlreadyInFridge = _rFridgeItem.Get(g => g.IsActive).Select(s => s.ItemId);
            var itemIdListToAdd = request.ItemIds.Where(w => !itemsAlreadyInFridge.Contains(w));
            
            var itemsToAddInFridge = _rItem.Get(g=>itemIdListToAdd.Contains(g.Id));
            
            await _rFridgeItem.AddRangeAsync(itemsToAddInFridge
                        .Select(s=> new FridgeItem(s.Name,
                            s.Color,
                            s.Expiration,
                            s.MinimunQuantity,
                            s.Quantity,
                            s.IconName,
                            s.Weight,
                            s.Id,
                            userFridge.Id,
                            request.UserId)
                        )
                );
            var success = await _rFridgeItem.SaveChangesAsync() > 0;
            if (!success)
                _logger.LogInformation("Items could not be added to fridge.");
            
            return new AddItemsToFridgeOut()
            {
                Success = true
            };

        }
        catch (Exception e)
        {
            _logger.LogError(e.Message,e.InnerException);
            throw;
        }
    }
}