using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrimatScheduleBot.Extensions;

namespace PrimatScheduleBot;

class Program
{
    static async Task Main()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Logging.AddAzureWebAppDiagnostics();

        builder.Services
            .AddAzureAuthentication(builder.Configuration)
            .AddWebhook(builder.Configuration)
            .AddRequestHandling()
            .AddSwagger();

        using var app = builder.Build();

        app.UseAuthentication();

        app.UseAuthorization();

        app.AddSwaggerSupport();

        //host.AddPost(botConfiguration);

        /*host.MapGet("/claims", (GetService getService) =>
        {
            string claims = getService.GetClaims();

            return Results.Ok(new { Value = claims });
        });*/

        await app.RunAsync();
    }
}
