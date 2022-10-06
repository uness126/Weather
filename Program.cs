using log4net.Core;
using Weather;
using Weather.Extensions;
using Weather.Helpers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllers();

#region Add Services

// Register OpenWeatherMap repository
DependencyContainer.RegisterServices(builder);

#endregion

#region Add Configure Logging

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddLog4Net("log4net.config");

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logger = app.Services.GetRequiredService<ILoggerFactory>();
app.ConfigureBuildInExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
