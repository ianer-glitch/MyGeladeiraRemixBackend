using Grpc.Net.Client;
using Identity.Domain.Ports;
using Identity.Grpc;
using Microsoft.AspNetCore.Mvc;


namespace Identity.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IIdentityGrpcConnection _con;  

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IIdentityGrpcConnection con)
    {
        _logger = logger;
        _con = con;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet]
    [Route("SendGreetingsMessage")]
    public async Task<ActionResult<string>> SendGreetingsMessage(string message)
    {
        try
        {

            var client =  _con.GetGrpcClient<GreeterUseCase.GreeterUseCaseClient>("http://user.service:8083");
            var request = new HelloRequest { Name = message };  
            return Ok(await client.SayHelloAsync(request));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest();
        }    
    }
}