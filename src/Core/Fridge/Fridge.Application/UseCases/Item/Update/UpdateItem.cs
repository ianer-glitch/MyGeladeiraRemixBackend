using Fridge.Domain.Items.Update;
using Microsoft.Extensions.Logging;

namespace Fridge.Application.UseCases.Item.Update;

public class UpdateItem : IUpdateItem
{
    private readonly IRepository<ItemModel, FridgeContext> _rItem;
    private readonly IRepository<FridgeItem, FridgeContext> _rFridgeItem;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;
    private readonly ILogger<UpdateItem> _logger;
    public UpdateItem(IRepository<ItemModel, FridgeContext> rItem, IFileAdapter<IFileAdapterResult> fileAdapter, IRepository<FridgeItem, FridgeContext> rFridgeItem, ILogger<UpdateItem> logger)
    {
        _rItem = rItem;
        _fileAdapter = fileAdapter;
        _rFridgeItem = rFridgeItem;
        _logger = logger;
    }
    public async  Task<IUpdateItemOut> ExecuteAsync(IUpdateItemIn request)
    {
        try
        {
            _logger.LogInformation("Updating item {Name}", request.Name);
            var currenctItem = await _rItem.Get(g => g.Id == request.ItemId).FirstOrDefaultAsync();

            if (currenctItem == null)
                throw new ArgumentNullException(nameof(currenctItem));
            
            currenctItem.Name = request.Name;   
            currenctItem.Color = request.Color;
            currenctItem.Quantity = request.Quantity;
            currenctItem.MinimunQuantity = request.MinimumQuantity;
            currenctItem.Expiration = request.Expiration;
            currenctItem.SetTimeToExpire(request.Expiration);
            
            
            if (request.Icon != null)
            {
                _logger.LogInformation("Updating item icon {Icon}", request.Icon);
                var fileResult = await _fileAdapter.UploadAsync(request.Icon);
                currenctItem.IconName = fileResult.Name; 
                _logger.LogInformation("Updated Icon :{Icon}", fileResult.Name);
            }
            
            
            _rItem.Update(currenctItem);
            
            var fridgeItemSucces= await UpdateFridgeItemsDependentsOnItem(request);
            var itemSucces = await _rItem.SaveChangesAsync() > 0 ;

            return new UpdateItemOut()
            {
                Success = fridgeItemSucces && itemSucces,
            };

        }
        catch (Exception e)
        {
            throw;
        }
    }

    private async Task<bool> UpdateFridgeItemsDependentsOnItem(IUpdateItemIn request)
    {
        try
        {
             await _rFridgeItem.Get(g => g.ItemId == request.ItemId)
                .ForEachAsync(async f =>
                {
                    f.Name = request.Name;
                    f.Color = request.Color;
                    if (request.Icon != null)
                    {
                        var fileResult = await _fileAdapter.UploadAsync(request.Icon);
                        f.IconName = fileResult.Name;
                    }
                    _rFridgeItem.Update(f);
                });

            return await _rFridgeItem.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}