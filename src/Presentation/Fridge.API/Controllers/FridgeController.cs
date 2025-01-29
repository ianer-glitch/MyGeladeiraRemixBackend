using Extensions;
using Fridge.Application.UseCases.Fridge.AddItem;
using Fridge.Application.UseCases.Fridge.GetItem;
using Fridge.Application.UseCases.Fridge.UpdateItem;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Fridges.UpdateItem;
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
    private readonly IGetFridgeItems _getFridgeItems;
    private readonly IUpdateFridgeItem _updateFridgeItem;
    public FridgeController(ILogger<FridgeController> logger, IAddItemsToFridge addItemsToFridge, IGetFridgeItems getFridgeItems, IUpdateFridgeItem updateFridgeItem)
    {
        _logger = logger;
        _addItemsToFridge = addItemsToFridge;
        _getFridgeItems = getFridgeItems;
        _updateFridgeItem = updateFridgeItem;
    }
    [HttpPost("items")]
    public async Task<ActionResult<AddItemsToFridgeOut>> AddItemsToFridge(AddItemsToFridgeIn request)
    {
        try
        {
            request.UserId = User.GetId();
            var result = await _addItemsToFridge.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }

    [HttpGet("items")]
    public async Task<ActionResult> GetFridgeItems()
    {
        try
        {
            var request = new GetFridgeItemsIn
            {
                UserId = User.GetId()
            };
            
            var result = await _getFridgeItems.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
    
    [HttpPut("items")]
    public async Task<ActionResult<IUpdateFridgeItemOut>> GetFridgeItems(UpdateFridgeItemIn request)
    {
        try
        {
            
            var result = await _updateFridgeItem.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
}