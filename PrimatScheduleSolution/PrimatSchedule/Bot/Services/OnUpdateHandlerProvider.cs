using Microsoft.Extensions.Logging;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using System.Collections.Generic;
using PrimatScheduleBot.ScheduleBot.Services.Interfaces;
using PrimatScheduleBot.Logging.Messages;
using System;

namespace PrimatScheduleBot.ScheduleBot.Services;

internal sealed class OnUpdateHandlerProvider
{
    private readonly Dictionary<UpdateType, IUpdateHandler> _updateHandlers;
    private readonly ILogger<OnUpdateHandlerProvider> _logger;

    public OnUpdateHandlerProvider(
        Dictionary<UpdateType, IUpdateHandler> updateHandlers,
        ILogger<OnUpdateHandlerProvider> logger)
    {
        _updateHandlers = updateHandlers;
        _logger = logger;
    }

    public IUpdateHandler Provide(Update update)
    {
        string updateTypeName = Enum.GetName(typeof(UpdateType), update.Type)!;

        _logger.LogInformation(Information.FetchingUpdateHandler(updateTypeName));
        
        return FetchHandler(update.Type);
    }

    private IUpdateHandler FetchHandler(UpdateType updateType)
    {
        return _updateHandlers.TryGetValue(updateType, out IUpdateHandler? typedHandler)
            ? typedHandler
            : _updateHandlers[UpdateType.Unknown];
    }
}
