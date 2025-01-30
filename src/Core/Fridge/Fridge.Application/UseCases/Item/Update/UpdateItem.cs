using Fridge.Domain.Items.Update;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItem : IUpdateItem
{
    private readonly IRepository<ItemModel, FridgeContext> _rItem;
    public UpdateItem(IRepository<ItemModel, FridgeContext> rItem)
    {
        _rItem = rItem;
    }
    public async  Task<IUpdateItemOut> ExecuteAsync(IUpdateItemIn request)
    {
        try
        {
            var currenctItem = await _rItem.Get(g => g.Id == request.ItemId).FirstOrDefaultAsync();

            if (currenctItem == null)
                throw new ArgumentNullException(nameof(currenctItem));
            
            currenctItem.Name = request.Name;   
            currenctItem.Color = request.Color;
            currenctItem.Quantity = request.Quantity;
            currenctItem.MinimunQuantity = request.Quantity;
            currenctItem.Expiration = request.Expiration;
            
            _rItem.Update(currenctItem);

            return new UpdateItemOut()
            {
                Success = await _rItem.SaveChangesAsync() > 0
            };

        }
        catch (Exception e)
        {
            throw;
        }
    }
}