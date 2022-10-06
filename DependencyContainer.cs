using Weather.Helpers;
using Weather.OpenWeatherMaps;
using Weather.Services;

namespace Weather;

public static class DependencyContainer
{
    public static void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IWeatherForecast, OpenWeatherMap>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.Configure<OpenWeatherMapViewModel>(
            builder.Configuration.GetSection(OpenWeatherMapViewModel.OpenWeatherMap));

        builder.Services.Configure<AppSettings>(
            builder.Configuration.GetSection(AppSettings.AppSetting));
    }
}
