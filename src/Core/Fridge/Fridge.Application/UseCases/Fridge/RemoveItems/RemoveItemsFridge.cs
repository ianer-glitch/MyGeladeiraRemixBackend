using Fridge.Domain.Fridges.RemoveItem;

namespace Fridge.Application.UseCases.Fridge.RemoveItems;

public class RemoveItemsFridge : IRemoveItemsFridge
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _rFridgeItem;
    private readonly IRepository<FridgeModel,FridgeContext> _rFridge;
    public RemoveItemsFridge(IRepository<FridgeItemModel, FridgeContext> rFridgeItem, IRepository<FridgeModel, FridgeContext> rFridge)
    {
        _rFridgeItem = rFridgeItem;
        _rFridge = rFridge;
    }
    
    public async Task<IRemoveItemsFridgeOut> ExecuteAsync(IRemoveItemsFridgeIn request)
    {
        try
        {
          
            await _rFridgeItem.Get(g => request.FridgeItemIds.Contains(g.Id))
                .ForEachAsync(f=>f.SetInactive()
                );
            return new RemoveItemsFridgeOut
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