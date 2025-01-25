using Fridge.Application.UseCases.Item.Create;
using Fridge.Application.UseCases.Item.Get;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Items.Get;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ICreateItem _createItem;
    private readonly IGetItems _getItems;   
    
    public ItemController(ICreateItem createItem , IGetItems getItems)
    {
        _createItem = createItem;   
        _getItems = getItems;
    }

    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CreateItemOut>> CreateItem(CreateItemIn input)
    {
        try
        {
            input.UserCreationId = Guid.NewGuid();
            var response =await _createItem.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<GetItemsOut>>> GetItems()
    {
        try
        {
            var input = new GetItemsIn();
            var response =await _getItems.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
    
}