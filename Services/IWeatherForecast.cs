using Weather.Models.Dto;

namespace Weather.Services;

public interface IWeatherForecast
{
    WeatherForecastDto? GetForecast(string cityName);
}
