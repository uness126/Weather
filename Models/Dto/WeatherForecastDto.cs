using System.ComponentModel.DataAnnotations;

namespace Weather.Models.Dto
{
    public class WeatherForecastDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float TemperatureC { get; set; }
        public string? Summary { get; set; }
    }
}
