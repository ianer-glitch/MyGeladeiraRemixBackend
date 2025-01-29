using Fridge.Domain.Items.Delete;

namespace Fridge.Application.UseCases.Item.Delete;

public class DeleteItem : IDeleteItem
{
    private readonly IRepository<ItemModel,FridgeContext> _rItem;
    public DeleteItem(IRepository<ItemModel, FridgeContext> rItem)
    {
        _rItem = rItem;
    }
    public async Task<IDeleteItemOut> ExecuteAsync(IDeleteItemIn request)
    {
        try
        {
            var item = await _rItem.Get(g=>g.Id == request.ItemId).FirstOrDefaultAsync();   
            
            ArgumentNullException.ThrowIfNull(item,nameof(item));
            
            item.SetInactive();
            
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