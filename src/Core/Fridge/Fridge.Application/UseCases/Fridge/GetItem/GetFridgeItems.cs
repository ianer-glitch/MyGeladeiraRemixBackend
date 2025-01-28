using Fridge.Domain.Fridges;
using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Ports.FileAdapter;
using Fridge.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Ports;

namespace Fridge.Application.UseCases.Fridge.GetItem;

public class GetFridgeItems : IGetFridgeItems
{
    private readonly IRepository<Domain.Fridges.Fridge, FridgeContext> _rFridge;
    private readonly IRepository<Domain.Fridges.FridgeItem, FridgeContext> _rFItem;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;
    
    public GetFridgeItems(
        IRepository<Domain.Fridges.Fridge, FridgeContext> rFridge,  
        IRepository<FridgeItem, FridgeContext> rFItem, IFileAdapter<IFileAdapterResult> fileAdapter)
    {
        _rFridge = rFridge;
        _rFItem = rFItem;
        _fileAdapter = fileAdapter;
    }
    public async Task<IEnumerable<IGetFridgeItemsOut>> ExecuteAsync(IGetFridgeItemsIn request)
    {
        try
        {
            var userFride = await _rFridge.Get(g=>g.UserId == request.UserId)
                                                 .AsNoTracking()
                                                 .FirstOrDefaultAsync();
            if (userFride == null)
                throw new ArgumentNullException(nameof(userFride));
            
            var items = _rFItem.Get(g=>g.FridgeId == userFride.Id)
                                                    .Select(s=> new GetFridgeItemsOut()
                                                    {
                                                        Color = s.Color,
                                                        IconLink = s.IconName,
                                                        PercentageExpired = s.GetPercentageExpired(),
                                                        ItemId = s.Id,
                                                        Quantity = s.Quantity,
                                                        
                                                    }).ToList();
            items.ForEach(async void (f) =>
                {
                    var link = (await _fileAdapter.GetFileAsync(f.IconLink)).Link ?? string.Empty;
                    f.IconLink = link;
                }
            );

            return items;


        }
        catch (Exception e)
        {
            throw;
        }
    }
}