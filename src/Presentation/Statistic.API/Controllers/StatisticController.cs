using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Statistic.Application.Statistics.GetByUser;
using Statistic.Domain.Statistics.GetByUser;

namespace Statistic.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class StatisticController : ControllerBase
{
    private readonly ILogger<StatisticController> _logger;
    private readonly IGetStatisticByUser _getStatisticByUser;
    public StatisticController(ILogger<StatisticController> logger, IGetStatisticByUser getStatisticByUser)
    {
        _logger = logger;
        _getStatisticByUser = getStatisticByUser;
    }

    [HttpGet("User")]
    public async Task<ActionResult<GetStatisticByUserOut>> GetStatisticByUser(GetStatisticByUserIn request)
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
}