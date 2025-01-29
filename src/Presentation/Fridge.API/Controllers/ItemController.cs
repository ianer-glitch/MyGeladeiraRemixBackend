using System.Security.Claims;
using Extensions;
using Fridge.Application.UseCases.Item.Create;
using Fridge.Application.UseCases.Item.Delete;
using Fridge.Application.UseCases.Item.Get;
using Fridge.Application.UseCases.Item.Update;
using Fridge.Domain.Items.Create;
using Fridge.Domain.Items.Delete;
using Fridge.Domain.Items.Get;
using Fridge.Domain.Items.Update;
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
    private readonly IUpdateItem _updateItem;
    private readonly IDeleteItem _deleteItem;
    private readonly ILogger<FridgeController> _logger;
    
    public ItemController(
        ICreateItem createItem ,
        IGetItems getItems,
        IUpdateItem updateItem,
        ILogger<FridgeController> logger ,
        IDeleteItem deleteItem)
    {
        _createItem = createItem;   
        _getItems = getItems;
        _updateItem = updateItem;
        _logger = logger;
        _deleteItem = deleteItem;
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<CreateItemOut>> CreateItem(CreateItemIn input)
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
    public async Task<ActionResult<UpdateItemOut>>UpdateItem(UpdateItemIn input)
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
    public async Task<ActionResult<DeleteItemOut>> DeleteItem(DeleteItemIn input)
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
    
}