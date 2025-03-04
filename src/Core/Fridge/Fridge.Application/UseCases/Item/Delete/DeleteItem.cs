using Fridge.Domain.Items.Delete;

namespace Fridge.Application.UseCases.Item.Delete;

public class DeleteItem : IDeleteItem
{
    private readonly IRepository<ItemModel,FridgeContext> _rItem;
    private readonly IRepository<FridgeItemModel,FridgeContext> _rFridgeItem;
    public DeleteItem(IRepository<ItemModel, FridgeContext> rItem, IRepository<FridgeItemModel, FridgeContext> rFridgeItem)
    {
        _rItem = rItem;
        _rFridgeItem = rFridgeItem;
    }
    public async Task<IDeleteItemOut> ExecuteAsync(IDeleteItemIn request)
    {
        try
        {
            var item = await _rItem.Get(g=>g.Id == request.ItemId).FirstOrDefaultAsync();  
            
            ArgumentNullException.ThrowIfNull(item,nameof(item));
            
            item.SetInactive();
            
            await _rFridgeItem.Get(g=>g.ItemId == request.ItemId)
                .ForEachAsync(f=>
                {
                    f.SetInactive();
                    _rFridgeItem.Update(f);
                });
            
            _rItem.Update(item);

            
            
            
            return new DeleteItemOut
            {
                Success = await _rItem.SaveChangesAsync() > 0
            };

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}