using Microsoft.Extensions.Logging;
using PrimatScheduleBot.ScheduleBot.Services.Interfaces;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using PrimatScheduleBot.Logging.Messages;

namespace PrimatScheduleBot.ScheduleBot.Services;

internal sealed class OnMessageReceivedHandler : IUpdateHandler
{
    private readonly ILogger _logger;

    public OnMessageReceivedHandler(ILogger<OnMessageReceivedHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update)
    {
        Message message = update.Message!;

        _logger.LogInformation(Information.MessageReceived(message.Text, message.Chat.Id));

        
    }
}
