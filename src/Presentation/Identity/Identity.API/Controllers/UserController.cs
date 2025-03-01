using Identity.API.Helpers;
using Identity.Domain.Ports;
using Identity.Domain.Login;
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
    public async Task<ActionResult<LoginOut>> Login(PIsUserPasswordValidIn request)
    {
        try
        {
        
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.IsUserPasswordValidAsync(request);
            
            var claims = await client.GetUserRolesAsync(new PGetUserRolesIn(){Email = request.Email});

            if (result is not null)
            {
                var resultoken = new LoginOut()
                {
                    Token = TokenHelpers.GenerateToken(
                        conf,
                        claims.Roles.Select(s => (string)s), Guid.Parse(result.UserId))
                };
                
                return Ok(resultoken);
                
            }
            
            return Empty;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<PCreateUserOut>> CreateUser([FromBody]PCreateUserIn request)
    {
        try
        {
            var client = _con.GetUserConnection<UserService.UserServiceClient>();
            var result = await client.CreateUserAsync(request);
            return Ok(result);
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