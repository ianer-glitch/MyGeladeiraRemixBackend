using Identity.API.Helpers;
using Identity.Domain.Ports;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(
    ILogger<UserController> logger,
    IConnectionHelper con,
    IConfiguration conf) : ControllerBase
{
    private readonly ILogger<UserController> _logger = logger;
    private readonly IConnectionHelper _con = con;
    

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(PIsUserPasswordValidIn request)
    {
        try
        {
        
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.IsUserPasswordValidAsync(request);
            if (result is not null)
                return Ok(TokenHelpers.GenerateToken(conf));
            
            return Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
    
    [HttpPost("CreateUser")]
    public async Task<ActionResult<string>> CreateUser([FromBody]PCreateUserIn request)
    {
        try
        {
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.CreateUserAsync(request);
            return Ok(result.Success);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
    
    [Authorize]
    [HttpDelete("DeleteUser")]
    public async Task<ActionResult<string>> DeleteUser([FromBody]PDeleteUserIn request)
    {
        try
        {
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.DeleteUserAsync(request);
            return Ok(result.Success);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
}