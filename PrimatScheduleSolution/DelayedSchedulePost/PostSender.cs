using System;
using System.Threading.Tasks;
using DataBaseExploitation.Models;
using DataBaseExploitation;
using Quartz;
using Telegram.Bot;

namespace DelayedSchedulePost
{
    public class PostSender : IJob
    {
        public string Token { get; set; }
        public string ChatId { get; set; }
        public async Task Execute(IJobExecutionContext context)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            var bot = new TelegramBotClient(Token);
            var chatId = long.Parse(ChatId);

            Schedule schedule = Querier.GetQuerySelection(date);

            await bot.SendTextMessageAsync(chatId, schedule.ToString(), Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }
    }
}
