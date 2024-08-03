using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using PrimatScheduleBot.Configuration;
using PrimatScheduleBot.ScheduleBot.Services;
using PrimatScheduleBot.ScheduleBot.Services.Interfaces;
using PrimatScheduleBot.Webhook;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using PrimatScheduleBot.Configuration.Models;
using Schedule.Bot.Firestore.Connection;

namespace PrimatScheduleBot.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(configuration);

        return services.AddAuthorization();
    }

    public static IServiceCollection AddWebhook(this IServiceCollection services, IConfiguration configuration)
    {
        BotConfiguration bot = new ConfigurationService(configuration)
            .GetSection<BotConfiguration>();

        services
            .AddHttpClient(bot.Webhook.Name)
            .AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(bot.Token, httpClient));

        return services;
    }

    /*public static IServiceCollection AddBotServices(this IServiceCollection services) =>
        services
            .AddSingleton<IBotEndpointService, TelegramBotService>()
            .AddScoped<IBotMessageService, TelegramBotService>()
            .AddScoped<ChatMessageService>()
            .AddScoped<GetService>();*/

    public static IServiceCollection AddRequestHandling(this IServiceCollection services) =>
        services
            .AddScoped<OnUpdateHandlerProvider>(sp =>
            {
                Dictionary<UpdateType, IUpdateHandler> updateHandlers = new()
                {
                    {
                        UpdateType.Message,
                        new OnMessageReceivedHandler(
                            logger: sp.GetRequiredService<ILogger<OnMessageReceivedHandler>>())
                    },
                    {
                        UpdateType.Unknown,
                        new OnUnknownUpdateReceivedHandler(
                            logger: sp.GetRequiredService<ILogger<OnUnknownUpdateReceivedHandler>>())
                    }
                };

                return new OnUpdateHandlerProvider(
                    updateHandlers: updateHandlers,
                    logger: sp.GetRequiredService<ILogger<OnUpdateHandlerProvider>>());
            })
            .AddHostedService<WebhookService>();

    public static IServiceCollection AddDbServices(this IServiceCollection services, IConfiguration configuration)
    {
        FirestoreAccessProvider accessProvider = new(
            connectionString: configuration.GetConnectionString("Firestore")!);

        services
            .AddSingleton(new FirestoreConnector(accessProvider).Connect())
            .AddSingleton();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services) =>
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
}
