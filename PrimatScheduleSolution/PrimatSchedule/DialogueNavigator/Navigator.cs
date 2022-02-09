using Telegram.Bot;
using Telegram.Bot.Args;
using System.Collections.Generic;
using System;

namespace PrimatScheduleBot
{
    public class Navigator
    {
        private readonly string _token;
        private readonly TelegramBotClient _bot;
        private readonly Dictionary<string, Func<long, string, string>> _dialogue;

        public Navigator(string token)
        {
            _token = token;
            _bot = new TelegramBotClient(_token);
            _dialogue = new Dictionary<string, Func<long, string, string>>
            {
                { Messages.Start, (long chatId, string token) => new Start(token, chatId).DoTaskAndGetMessage() },
                { Messages.Stop, (long chatId, string token) => new Stop(chatId).DoTaskAndGetMessage() },
                { Messages.Help, (long chatId, string token) => new Help().DoTaskAndGetMessage() },
                { Messages.Date, (long chatId, string date) => new Date(date).DoTaskAndGetMessage() }
            };
        }

        public void StartChating()
        {
            _bot.StartReceiving();
            _bot.OnMessage += ReceiveMessage;
        }

        private void ReceiveMessage(object sender, MessageEventArgs arguments)
        {
            long chatId = arguments.Message.Chat.Id;
            string message = arguments.Message.Text;

            string answer = getAnswer(message, chatId);

            _bot.SendTextMessageAsync(chatId, answer, Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }

        private string getAnswer(string message, long chatId)
        {
            try
            {
                return _dialogue[message](chatId, _token);
            }
            catch
            {
                return _dialogue[Messages.Date](chatId, message);
            }
        }
    }
}
