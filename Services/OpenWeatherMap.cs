using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Weather.Models.Dto;
using Weather.OpenWeatherMaps;

namespace Weather.Services;

public class OpenWeatherMap : IWeatherForecast
{
    private readonly OpenWeatherMapViewModel _openWeatherMapOptions;

    public OpenWeatherMap(IOptions<OpenWeatherMapViewModel> openWeatherMapOptions)
    {
        _openWeatherMapOptions = openWeatherMapOptions.Value;
    }

    public WeatherForecastDto? GetForecast(string cityName)
    {
        var options = new RestClientOptions()
        {
            Timeout = -1
        };

        var client = new RestClient(options);

        var request = new RestRequest($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={_openWeatherMapOptions.Api}&units=metric", Method.Get);

        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            var content = JsonConvert.DeserializeObject<JToken>(response.Content);

            var result = content.ToObject<WeatherResponse>();

            return new WeatherForecastDto()
            {
                Id = result.Id,
                Date = DateTime.Now,
                TemperatureC = result.Main.Temp,
                Summary = $"Main: {result.Weather.First().Main}, Description: {result.Weather.First().Description}"
            };
        }

        return null;
    }
}
