using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fridge.API.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]

public class FridgeController : ControllerBase
{
    // [HttpPost("add/item")]
    // public async Task<ActionResult<bool>> AddItemToFridge()
    // {
    //     
    // }
}