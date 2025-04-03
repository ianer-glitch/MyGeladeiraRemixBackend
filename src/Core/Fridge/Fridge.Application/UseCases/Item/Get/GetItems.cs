using Microsoft.Extensions.Logging;

namespace Fridge.Application.UseCases.Item.Get;

public class GetItems : IGetItems
{
    private readonly IRepository<ItemModel, FridgeContext> _repository;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;
    private readonly ILogger<GetItems> _logger;
    
    public GetItems(IRepository<ItemModel, FridgeContext> repository ,IFileAdapter<IFileAdapterResult> fileAdapter, ILogger<GetItems> logger)
    {
        _repository = repository;
        _fileAdapter = fileAdapter;
        _logger = logger;
    }
    public async Task<IEnumerable<IGetItemsOut>> ExecuteAsync(IGetItemsIn request)
    {
        try
        {
            _logger.LogInformation("Get Items");
            var items =_repository.Get(x=>x.IsActive).ToList();
            var list = items.Select(s => new GetItemsOut
            {
                Id = s.Id,
                Name = s.Name,
                Icon = s.IconName,
                Color = s.Color,
                Expiration = s.CurrecntExpirationDate,
                Quantity = s.Quantity,
                MinimumQuantity = s.MinimunQuantity,
                Weight = s.Weight,  
            }).ToList();

            list.ForEach(async void (f) =>
                {
                    var link = (await _fileAdapter.GetFileAsync(f.Icon)).Link ?? string.Empty;
                    f.Icon = link;
                }
            );

            return list;
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not GetItems {Message},{InnerException}",ex.Message, ex.InnerException);
            throw;
        }
    }
}