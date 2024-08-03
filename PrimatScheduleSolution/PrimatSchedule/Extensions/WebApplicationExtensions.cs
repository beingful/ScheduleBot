using Microsoft.AspNetCore.Builder;

namespace PrimatScheduleBot.Extensions;

internal static class WebApplicationExtensions
{
    public static WebApplication AddSwaggerSupport(this WebApplication app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bussy Piggy Bot API");
            options.RoutePrefix = string.Empty;
        });

        return app;
    }
}
