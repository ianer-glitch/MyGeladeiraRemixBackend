using Fridge.Domain.Items.Update;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItem : IUpdateItem
{
    private readonly IRepository<ItemModel, FridgeContext> _rItem;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;
    public UpdateItem(IRepository<ItemModel, FridgeContext> rItem, IFileAdapter<IFileAdapterResult> fileAdapter)
    {
        _rItem = rItem;
        _fileAdapter = fileAdapter;
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
            if (request.Icon != null)
            {
                var fileResult = await _fileAdapter.UploadAsync(request.Icon);
                currenctItem.IconName = fileResult.Name; 
                
            }
            
            
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