using Microsoft.Extensions.Logging;
using PrimatScheduleBot.Logging.Messages;
using PrimatScheduleBot.ScheduleBot.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PrimatScheduleBot.ScheduleBot.Services;

internal sealed class OnUnknownUpdateReceivedHandler : IUpdateHandler
{
    private ILogger _logger;

    public OnUnknownUpdateReceivedHandler(ILogger<OnUnknownUpdateReceivedHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Update update) =>
        await Task.Run(() =>
        {
            string updateTypeName = Enum.GetName(typeof(UpdateType), update.Type)!;

            _logger.LogInformation(Information.UnknownUpdate(updateTypeName));
        });
}
