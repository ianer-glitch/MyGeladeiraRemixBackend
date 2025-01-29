

namespace Fridge.Application.UseCases.Fridge.UpdateItem;

public class UpdateFridgeItem : IUpdateFridgeItem
{
    private readonly IRepository<FridgeItem,FridgeContext> _repository;
    
    public UpdateFridgeItem(IRepository<FridgeItem, FridgeContext> repository)
    {
        _repository = repository;
    }
    public async Task<IUpdateFridgeItemOut> ExecuteAsync(IUpdateFridgeItemIn request)
    {
        try
        {
            var currenctItem =  await _repository.Get(g => g.Id == request.ItemId).FirstOrDefaultAsync();
            
            if(currenctItem == null)
                throw new ArgumentNullException(nameof(currenctItem));    
            
            currenctItem.Modified = DateTime.UtcNow;
            currenctItem.Quantity = request.Quantity;
            currenctItem.MinimunQuantity = request.Quantity;    
            
            _repository.Update(currenctItem);
            
            return new UpdateFridgeItemOut()
            {
                Success = await _repository.SaveChangesAsync() > 0,
            };

        }
        catch (Exception e)
        {
            throw;
        }
    }
}