using Fridge.Domain.Fridges;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Infrastructure;
using Ports;

namespace Fridge.Application.UseCases.Fridge.AddItem;

public class AddItemsToFridge : IAddItemsToFridge
{
    private readonly IRepository<FridgeItem,FridgeContext> _rFridgeItem;
    private readonly IRepository<Domain.Fridges.Fridge,FridgeContext> _rFridge;
    private readonly IRepository<Domain.Items.Item,FridgeContext> _rItem;


    public AddItemsToFridge(IRepository<FridgeItem, FridgeContext> rFridgeItem,
        IRepository<Domain.Fridges.Fridge, FridgeContext> rFridge,
        IRepository<Domain.Items.Item, FridgeContext> rItem)
    {
        _rFridgeItem = rFridgeItem;
        _rFridge = rFridge;
        _rItem = rItem;
    }
    
    public async Task<IAddItemsToFridgeOut> ExecuteAsync(IAddItemsToFridgeIn request)
    {
        try
        {
            var userFridge =  _rFridge.Get(g=>g.UserId == request.UserId).FirstOrDefault();
            if (userFridge == null)
            {
                userFridge = new Domain.Fridges.Fridge(request.UserId,request.UserId);
                await _rFridge.InsertAsync(userFridge);
                if(await _rFridge.SaveChangesAsync() == 0)
                    throw new  Exception("Could not create Fridge for user" + request.UserId);
            }
            var itemsToAddInFridge = _rItem.Get(g=>request.ItemIds.Contains(g.Id));
            
            await _rFridgeItem.AddRangeAsync(itemsToAddInFridge
                        .Select(s=> new FridgeItem(s.Name,
                            s.Color,
                            s.Expiration,
                            s.MinimunQuantity,
                            s.Quantity,
                            s.IconName
                            ,request.UserId
                            ,userFridge.Id)
                        )
                );

            return new AddItemsToFridgeOut()
            {
                Success = await _rFridgeItem.SaveChangesAsync() > 0
            };

        }
        catch (Exception e)
        {
            throw;
        }
    }
}