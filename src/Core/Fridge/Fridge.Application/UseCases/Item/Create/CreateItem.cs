using Fridge.Domain.Items.Create;
using Fridge.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Ports;
using ItemModel = Fridge.Domain.Items.Item;

namespace Fridge.Application.UseCases.Item.Create;

public class CreateItem : ICreateItem
{
    private readonly FridgeContext _context;
    private readonly IRepository<ItemModel,FridgeContext> _repository;   
    

    public CreateItem(FridgeContext context, IRepository<ItemModel,FridgeContext> repository)
    {
        _context = context; 
        _repository = repository;   
    }


    public async  Task<ICreateItemOut> ExecuteAsync(ICreateItemIn request)
    {
        try
        {
            var isExistingItem = _repository.Get(g=>g.Name == request.Name).Any();
            if(isExistingItem)
                throw new ArgumentException($"Item {request.Name} already exists");

            var item = new ItemModel(request.Name,
                                     request.Color,
                                     request.Expiration,
                                     request.MinimumQuantity,
                                     request.DefaultQuantity,
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