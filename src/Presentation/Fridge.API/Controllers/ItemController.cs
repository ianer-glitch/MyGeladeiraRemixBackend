using Fridge.Application.UseCases.Item.Create;
using Fridge.Domain.Items.Create;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ICreateItem _createItem;
    
    public ItemController(ICreateItem createItem)
    {
        _createItem = createItem;   
    }

    [HttpPost("Item")]
    public async Task<ActionResult<CreateItemOut>> CreateItem(CreateItemIn input)
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