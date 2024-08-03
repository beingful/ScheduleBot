using System.Threading.Tasks;
using System.Threading;

namespace PrimatScheduleBot.Webhook.Interfaces;

internal interface IEndpointManager
{
    public string Url { get; }

    public Task StartAsync(CancellationToken cancellationToken);

    public Task StopAsync(CancellationToken cancellationToken);
}
