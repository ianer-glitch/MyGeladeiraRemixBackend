
namespace Fridge.Application.UseCases.Item.Create;

public class CreateItem : ICreateItem
{
    private readonly FridgeContext _context;
    private readonly IRepository<ItemModel,FridgeContext> _repository;
    private readonly IFileAdapter<IFileAdapterResult> _fileAdapter;

    public CreateItem(
        FridgeContext context,
        IRepository<ItemModel,FridgeContext> repository,
        IFileAdapter<IFileAdapterResult> fileAdapter)
    {
        _context = context; 
        _repository = repository;
        _fileAdapter = fileAdapter;
    }


    public async  Task<ICreateItemOut> ExecuteAsync(ICreateItemIn request)
    {
        try
        {
            var isExistingItem = _repository.Get(g=>g.Name == request.Name).Any();
            if(isExistingItem)
                throw new ArgumentException($"Item {request.Name} already exists");
        
            var fileResult = await _fileAdapter.UploadAsync(request.Icon);
            
            var item = new ItemModel(request.Name,
                                     request.Color,
                                     request.Expiration.ToUniversalTime(),
                                     request.MinimumQuantity,
                                     request.Quantity,
                                     fileResult.Name,
                                     request.UserCreationId);
            
            await _repository.InsertAsync(item);
            
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
                return new CreateItemOut { Item = item };
            
            throw new Exception("Could not create item");   


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


}