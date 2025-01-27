using Fridge.Application.UseCases.Fridge.AddItem;
using Fridge.Domain.Fridges.AddItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]

public class FridgeController : ControllerBase
{

    private readonly ILogger<FridgeController> _logger;
    private readonly IAddItemsToFridge  _addItemsToFridge;
    public FridgeController(ILogger<FridgeController> logger, IAddItemsToFridge addItemsToFridge)
    {
        _logger = logger;
        _addItemsToFridge = addItemsToFridge;
    }
    [HttpPost("add/item")]
    public async Task<ActionResult<AddItemsToFridgeOut>> AddItemToFridge(AddItemsToFridgeIn request)
    {
        try
        {
            var result = await _addItemsToFridge.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
}