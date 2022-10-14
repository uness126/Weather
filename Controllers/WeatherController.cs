using Microsoft.AspNetCore.Mvc;
using Weather.Helpers;
using Weather.Models;
using Weather.Services;

namespace Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IUserService _userService;
    private readonly IWeatherForecast _weatherForecast;

    public WeatherController(ILogger<WeatherController> logger, IUserService userService, IWeatherForecast weatherForecast)
    {
        _logger = logger;
        _userService = userService;
        _weatherForecast = weatherForecast;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);

        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
    }

    [Authorize]
    [HttpGet("[action]/{city}")]
    [ResponseCache(CacheProfileName = "Cache1min")]
    public IActionResult Get(string city)
    {
        var weather = _weatherForecast.GetForecast(city);

        _logger.LogInformation(weather == null ? "Not found!" :
            $"City: {city} TemperatureC: {weather.TemperatureC} Summary: {weather.Summary}");

        return Ok(weather);
    }
}