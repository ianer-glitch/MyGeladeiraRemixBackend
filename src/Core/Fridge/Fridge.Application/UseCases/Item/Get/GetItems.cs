namespace Fridge.Application.UseCases.Item.Get;

public class GetItems : IGetItems
{
    private readonly IRepository<ItemModel, FridgeContext> _repository;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;
    
    public GetItems(IRepository<ItemModel, FridgeContext> repository ,IFileAdapter<IFileAdapterResult> fileAdapter)
    {
        _repository = repository;
        _fileAdapter = fileAdapter;
    }
    public async Task<IEnumerable<IGetItemsOut>> ExecuteAsync(IGetItemsIn request)
    {
        try
        {
            var items =_repository.Get(x=>true);
            var list = items.Select(s => new GetItemsOut
            {
                Id = s.Id,
                Name = s.Name,
                Icon = s.IconName,
                Color = s.Color,
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
            throw;
        }
    }
}