using Microsoft.AspNetCore.Http;

namespace Fridge.Domain.Items.Get;

public interface IGetItems
{
    public  Task<IEnumerable<IGetItemsOut>> ExecuteAsync(IGetItemsIn request); 
}