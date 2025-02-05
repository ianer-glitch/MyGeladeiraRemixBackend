using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Statistic.Application.Statistics.GetByAllUser;
using Statistic.Application.Statistics.GetByUser;
using Statistic.Domain.Statistics.GetByAllUser;
using Statistic.Domain.Statistics.GetByUser;

namespace Statistic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class StatisticController : ControllerBase
{
    private readonly ILogger<StatisticController> _logger;
    private readonly IGetStatisticByUser _getStatisticByUser;
    private readonly IGetStatisticByAllUser _getStatisticByAllUser;
    public StatisticController(ILogger<StatisticController> logger, IGetStatisticByUser getStatisticByUser, IGetStatisticByAllUser getStatisticByAllUser)
    {
        _logger = logger;
        _getStatisticByUser = getStatisticByUser;
        _getStatisticByAllUser = getStatisticByAllUser;
    }

    [HttpGet("User")]
    public async Task<ActionResult<GetStatisticByUserOut>> GetStatisticByAllUser(GetStatisticByUserIn request)
    {
        try
        {
            var result = await _getStatisticByUser.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
    
    [HttpGet("AllUser")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<GetStatisticByAllUserOut>> GetStatisticByUser(GetStatisticByAllUserIn request)
    {
        try
        {
            var result = await _getStatisticByAllUser.ExecuteAsync(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest();
        }
    }
}