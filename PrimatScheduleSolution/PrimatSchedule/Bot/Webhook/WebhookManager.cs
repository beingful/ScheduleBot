using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using System;
using PrimatScheduleBot.Webhook.Interfaces;
using PrimatScheduleBot.Configuration.Models;

namespace PrimatScheduleBot.Bot.Webhook;

internal sealed class WebhookManager : IEndpointManager
{
    private readonly WebhookConfiguration _webhook;
    private readonly ITelegramBotClient _botClient;

    public WebhookManager(WebhookConfiguration webhook, ITelegramBotClient botClient)
    {
        _webhook = webhook;
        _botClient = botClient;
    }

    string IEndpointManager.Url => _webhook.Url;

    async Task IEndpointManager.StartAsync(CancellationToken cancellationToken) =>
        await _botClient.SetWebhookAsync(
            url: _webhook.Url,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);

    async Task IEndpointManager.StopAsync(CancellationToken cancellationToken) =>
        await _botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
}