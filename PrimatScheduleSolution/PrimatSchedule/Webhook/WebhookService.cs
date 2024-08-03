using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrimatScheduleBot.Logging.Messages;
using PrimatScheduleBot.Webhook.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrimatScheduleBot.Webhook;

internal sealed class WebhookService : IHostedService
{
    private readonly IEndpointManager _entryPointManager;
    private readonly ILogger<WebhookService> _logger;

    public WebhookService(IEndpointManager entryPointManager, ILogger<WebhookService> logger)
    {
        _entryPointManager = entryPointManager;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _entryPointManager.StartAsync(cancellationToken);

            _logger.LogInformation(Information.ServiceStarted(_entryPointManager.Url));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, Errors.FailedToStartService(_entryPointManager.Url));

            throw;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _entryPointManager.StopAsync(cancellationToken);

            _logger.LogInformation(Information.ServiceStopped(_entryPointManager.Url));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, Errors.FailedToStopService(_entryPointManager.Url));

            throw;
        }
    }
}
