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
                { Messages.Start, (long chatId, string token) => new Start(token, chatId).HandleAndSendAnswer() },
                { Messages.Stop, (long chatId, string token) => new Stop(chatId).HandleAndSendAnswer() },
                { Messages.Help, (long chatId, string token) => new Help().HandleAndSendAnswer() },
                { Messages.Date, (long chatId, string date) => new Date(date).HandleAndSendAnswer() }
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

            SendAnswer(message, chatId);
        }

        private void SendAnswer(string message, long chatId)
        {
            string answer = String.Empty;

            try
            {
                answer = _dialogue[message](chatId, _token);
            }
            catch
            {
                answer = _dialogue[Messages.Date](chatId, message);
            }

            _bot.SendTextMessageAsync(chatId, answer, Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }
    }
}
