using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace PrimatScheduleBot
{
    public class PostSender : IJob
    {
        private const string _key = "_data";

        public async Task Execute(IJobExecutionContext context)
        {
            PostSenderData data = GetData(context);
            IEnumerable<Event> schedule = GetSchedule(data.ChatId);
            string message = ScheduleToMessage(schedule);

            await SendSchedule(data, message);
        }

        private string ScheduleToMessage(IEnumerable<Event> schedule)
        {
            var converter = new ScheduleToMessage(schedule);

            return converter.Convert();
        }

        private PostSenderData GetData(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            return (PostSenderData)dataMap[_key];
        }

        private IEnumerable<Event> GetSchedule(string chatId)
        {
            using var facade = new EventFacade();

            var date = DateTime.Today.AddDays(1);

            return facade.GetByDate(chatId, date);
        }

        private async Task SendSchedule(PostSenderData data, string message)
        {
            var bot = new TelegramBotClient(data.Token);

            await bot.SendTextMessageAsync(data.ChatId, message, ParseMode.Markdown, null, true);
        }
    }
}
