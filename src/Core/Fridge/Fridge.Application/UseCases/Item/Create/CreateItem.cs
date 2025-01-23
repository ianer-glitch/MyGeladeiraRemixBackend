using Fridge.Domain.Items;

using Fridge.Domain.Items.Create;
using Ports;

namespace Fridge.Application.UseCases.Item.Create;

public class CreateItem : ICreateItem<CreateItemIn,CreateItemOut>
{
    
    

    public CreateItem()
    {
        
    }


    public Task<CreateItemOut> Execute(CreateItemIn request)
    {
        throw new NotImplementedException();
    }
}