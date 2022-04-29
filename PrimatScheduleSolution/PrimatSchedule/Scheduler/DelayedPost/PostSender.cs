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

        private UI GetUI(IEnumerable<Event> schedule)
        {
            UI ui;

            try
            {
                string message = ScheduleToMessage(schedule);

                ui = new UI(message, Stickers.Walking);
            }
            catch (MessageException exception)
            {
                ui = exception.UI;
            }

            return ui;
        }

        private string ScheduleToMessage(IEnumerable<Event> schedule)
        {
            var converter = new ScheduleToMessage(schedule);
            string a = converter.Convert();

            return a;
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

        private async Task SendSchedule(PostSenderData data, UI ui)
        {
            var bot = new TelegramBotClient(data.Token);

            await bot.SendStickerAsync(data.ChatId, ui.StickerId);
            await bot.SendTextMessageAsync(data.ChatId, ui.Question, ParseMode.Markdown, null, true);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            PostSenderData data = GetData(context);
            IEnumerable<Event> schedule = GetSchedule(data.ChatId);
            UI ui = GetUI(schedule);

            await SendSchedule(data, ui);
        }
    }
}