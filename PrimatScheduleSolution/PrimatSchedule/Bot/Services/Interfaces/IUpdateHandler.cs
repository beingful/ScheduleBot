using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PrimatScheduleBot.ScheduleBot.Services.Interfaces;

internal interface IUpdateHandler
{
    public Task HandleAsync(Update update);
}
