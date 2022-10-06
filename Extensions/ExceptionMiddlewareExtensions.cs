using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Weather.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var _logger = loggerFactory.CreateLogger("exceptionhandlermiddleware");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                var contextRequest = context.Features.Get<IHttpRequestFeature>();

                if (contextFeature != null)
                {
                    string _error = $"[{context.Response.StatusCode}] - {contextFeature.Error.Message}: {contextRequest.Path}";

                    _logger.LogError(_error);

                    await context.Response.WriteAsync(_error);
                }
            });
        });
    }
}
