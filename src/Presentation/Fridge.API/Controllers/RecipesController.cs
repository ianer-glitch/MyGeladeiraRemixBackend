using Extensions;
using Fridge.Application.UseCases.Recipe.AskRecipe;
using Fridge.Domain.Recipes.AskRecipes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]

public class RecipesController : ControllerBase
{
    private readonly ILogger<RecipesController> _logger;
    private readonly IAskRecipes _askRecipes;
    public RecipesController(ILogger<RecipesController> logger, IAskRecipes askRecipes)
    {
        _logger = logger;
        _askRecipes = askRecipes;
    }

    [HttpGet()]
    public async Task<ActionResult<List<AskRecipesOut>>> AskRecipes()
    {
        try
        {
            var request = new AskRecipesIn
            {
                UserId = User.GetId()
            };
            
            var result = await _askRecipes.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);   
            return BadRequest();    
        }
    }
}