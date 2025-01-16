using Identity.Domain.Ports;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, IConnectionHelper con) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IConnectionHelper _con = con;

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(PLoginIn request)
    {
        try
        {
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.LoginAsync(request); 
            return Ok(result.Token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
}