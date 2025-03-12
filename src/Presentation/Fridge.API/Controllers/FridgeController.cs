using Extensions;
using Fridge.Application.UseCases.Fridge.AddItem;
using Fridge.Application.UseCases.Fridge.GetItem;
using Fridge.Application.UseCases.Fridge.GetRecommendedItem;
using Fridge.Application.UseCases.Fridge.RemoveItems;
using Fridge.Application.UseCases.Fridge.UpdateItem;
using Fridge.Application.UseCases.Fridge.UpdateMultipleItemQuantity;
using Fridge.Domain.Fridges.AddItem;
using Fridge.Domain.Fridges.GetItem;
using Fridge.Domain.Fridges.GetRecommendedItem;
using Fridge.Domain.Fridges.RemoveItem;
using Fridge.Domain.Fridges.UpdateItem;
using Fridge.Domain.Fridges.UpdateMultipleItemQuantity;
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
    private readonly IUpdateMultipleFridgeItemsQuantities _updateMultipleFridgeItemsQuantities;
    private readonly IRemoveItemsFridge _removeItemsFridge;
    private readonly IGetRecommendedItems _getRecommendedItems;
    
    public FridgeController(
        ILogger<FridgeController> logger,
        IAddItemsToFridge addItemsToFridge,
        IGetFridgeItems getFridgeItems,
        IUpdateFridgeItem updateFridgeItem, 
        IUpdateMultipleFridgeItemsQuantities updateMultipleFridgeItemsQuantities, IRemoveItemsFridge removeItemsFridge, IGetRecommendedItems getRecommendedItems)
    {
        _logger = logger;
        _addItemsToFridge = addItemsToFridge;
        _getFridgeItems = getFridgeItems;
        _updateFridgeItem = updateFridgeItem;
        _updateMultipleFridgeItemsQuantities = updateMultipleFridgeItemsQuantities;
        _removeItemsFridge = removeItemsFridge;
        _getRecommendedItems = getRecommendedItems;
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
    public async Task<ActionResult<GetFridgeItemsOut>> GetFridgeItems()
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
    public async Task<ActionResult<UpdateFridgeItemOut>> UpdateFridgeItem(UpdateFridgeItemIn request)
    {
        try
        {
            request.UserId = User.GetId();
            var result = await _updateFridgeItem.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
    
    [HttpPatch("items")]
    public async Task<ActionResult<UpdateMultipleFridgeItemsQuantitiesOut>>UpdateMultipleFridgeItemsQuantities(IEnumerable<UpdateMultipleFridgeItemsQuantitiesIn> request)
    {
        try
        {
            request.ToList().ForEach(a=>a.UserId = User.GetId());
            var result = await _updateMultipleFridgeItemsQuantities.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }

    [HttpDelete("items")]
    public async Task<ActionResult<RemoveItemsFridgeOut>> RemoveItemsFridge(RemoveItemsFridgeIn request)
    {
        try
        {
            request.UserId = User.GetId();
            var result = await _removeItemsFridge.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
    
    [HttpGet("recommended-items/{responseLanguage}")]
    public async Task<ActionResult<List<GetRecommendedItemsOut>>> GetRecommendedItems( string responseLanguage)
    {
        try
        {
            var request = new GetRecommendedItemsIn
            {
                ResponseLanguage = responseLanguage,
                UserId = User.GetId()
                
            };
            
            var result = await _getRecommendedItems.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
    
    
}