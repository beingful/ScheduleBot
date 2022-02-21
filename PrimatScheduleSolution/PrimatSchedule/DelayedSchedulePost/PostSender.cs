using System;
using System.Threading.Tasks;
using Quartz;
using Telegram.Bot;

namespace PrimatScheduleBot
{
    public class PostSender : IJob
    {
        public string Token { get; set; }
        public string ChatId { get; set; }
        public async Task Execute(IJobExecutionContext context)
        {
            var bot = new TelegramBotClient(Token);
            var chatId = long.Parse(ChatId);

            Schedule schedule = Querier.GetScheduleByDate(ChatId, DateTime.Today);

            await bot.SendTextMessageAsync(chatId, schedule.ToString(), Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }
    }
}
