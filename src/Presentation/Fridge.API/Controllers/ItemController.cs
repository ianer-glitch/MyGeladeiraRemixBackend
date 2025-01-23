using Fridge.Domain.Items.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ICreateItem<ICreateItemIn, ICreateItemOut> _createItem;
    
    public ItemController(ICreateItem<ICreateItemIn,ICreateItemOut> createItem)
    {
        _createItem = createItem;   
    }

    [HttpPost("Item")]
    public async Task<ActionResult<ICreateItemOut>> CreateItem(ICreateItemIn input)
    {
        try
        {
            var response =await _createItem.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}