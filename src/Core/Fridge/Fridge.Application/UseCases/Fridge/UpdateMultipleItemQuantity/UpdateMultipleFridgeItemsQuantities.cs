using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;

namespace Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;

public class UpdateMultipleFridgeItemsQuantities : IUpdateMultipleFridgeItemsQuantities
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _fridgeItemRepository;
    public UpdateMultipleFridgeItemsQuantities(IRepository<FridgeItemModel,FridgeContext> fridgeItemRepository)
    {
        _fridgeItemRepository = fridgeItemRepository;
    }
    public async Task<IUpdateMultipleFridgeItemsQuantitiesOut> ExecuteAsync(IEnumerable<IUpdateMultipleFridgeItemsQuantitiesIn> request)
    {
        try
        {
            var items  =_fridgeItemRepository.Get(g => 
                                                                                request.Select(s => s.ItemId)
                                                                                       .Contains(g.Id));

            (from req in request
                join ite in items on req.ItemId equals ite.Id
                select new
                {
                   req,ite 
                }).ToList().ForEach(f =>
                {
                    f.ite.Quantity =f.req.Quantity;    
                });
            
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