using Extensions;
using Fridge.Application.UseCases.ShoppingList.GetItems;
using Fridge.Domain.ShoppingLists.GetItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ShoppingListController : ControllerBase
{
    private readonly ILogger<ShoppingListController> _logger;
    private readonly IGetItemsShoppingList _getItemsShoppingList;
    public ShoppingListController(ILogger<ShoppingListController> logger, IGetItemsShoppingList getItemsShoppingList)
    {
        _logger = logger;
        _getItemsShoppingList = getItemsShoppingList;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetItemsShoppingListOut>>> GetItemsShoppingList()
    {
        try
        {
            var request = new GetItemsShoppingListIn
            {
                UserId = User.GetId()
            };
            var result = await _getItemsShoppingList.ExecuteAsync(request);
            return Ok(result);  
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest();
        }
    }
}