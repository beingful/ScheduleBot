using Microsoft.AspNetCore.Http;
using PrimatScheduleBot.ScheduleBot.Update;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PrimatScheduleBot.Controllers;

internal sealed class BotController
{
    public async Task<IResult> AnswerAsync(ITelegramBotClient botClient, HttpRequest request,
        OnUpdateService onUpdateService, Update update)
    {

        return Results.Ok();
    }
}
