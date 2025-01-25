using Fridge.Application.UseCases.Item.Create;
using Fridge.Domain.Items.Get;
using Fridge.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Ports;
using ItemModel = Fridge.Domain.Items.Item;


namespace Fridge.Application.UseCases.Item.Get;

public class GetItems : IGetItems
{
    private readonly IRepository<ItemModel, FridgeContext> _repository;
    
    public GetItems(IRepository<ItemModel, FridgeContext> repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<IGetItemsOut>> ExecuteAsync(IGetItemsIn request)
    {
        try
        {
            var items =_repository.Get(x=>true);
            return await items.Select(s => new GetItemsOut
            {
                Id = s.Id,
                Name = s.Name,
                Icon = s.IconName,
                Color = s.Color,
            }).ToListAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}