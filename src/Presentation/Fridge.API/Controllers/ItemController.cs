using System.Security.Claims;
using Extensions;
using Fridge.Application.UseCases.Fridge.GetRecommendedItem;
using Fridge.Application.UseCases.Item.Create;
using Fridge.Application.UseCases.Item.Delete;
using Fridge.Application.UseCases.Item.Get;
using Fridge.Application.UseCases.Item.GetRecommendedWeight;
using Fridge.Application.UseCases.Item.Update;
using Fridge.Domain.Fridges.GetRecommendedItem;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Items.Delete;
using Fridge.Domain.Items.Get;
using Fridge.Domain.Items.GetRecommendedWeight;
using Fridge.Domain.Items.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]

[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ICreateItem _createItem;
    private readonly IGetItems _getItems;   
    private readonly IUpdateItem _updateItem;
    private readonly IDeleteItem _deleteItem;
    private readonly ILogger<FridgeController> _logger;
    public readonly IGetRecommendedItemWeight _getRecommendedItemWeight;
    
    public ItemController(
        ICreateItem createItem ,
        IGetItems getItems,
        IUpdateItem updateItem,
        ILogger<FridgeController> logger ,
        IDeleteItem deleteItem, IGetRecommendedItemWeight getRecommendedItemWeight)
    {
        _createItem = createItem;   
        _getItems = getItems;
        _updateItem = updateItem;
        _logger = logger;
        _deleteItem = deleteItem;
        _getRecommendedItemWeight = getRecommendedItemWeight;
        
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CreateItemOut>> CreateItem([FromForm]CreateItemIn input)
    {
        try
        {
            
            input.UserCreationId = User.GetId();
            var response =await _createItem.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    
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
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<UpdateItemOut>>UpdateItem([FromForm]UpdateItemIn input)
    {
        try
        {
            var response =await _updateItem.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<DeleteItemOut>> DeleteItem([FromBody]DeleteItemIn input)
    {
        try
        {
            var response =await _deleteItem.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    
    [HttpGet("recommended/{name}")]
    [Authorize(Roles = "Administrator")]
    
    public async Task<ActionResult<IEnumerable<GetRecommendedItemWeightOut>>> GetRecommendedItemWeight(string name)
    {
        try
        {
            var input = new GetRecommendedItemWeightIn
            {
                Name = name
            };
            var response =await _getRecommendedItemWeight.ExecuteAsync(input);
            return Ok(response);    
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
    
}