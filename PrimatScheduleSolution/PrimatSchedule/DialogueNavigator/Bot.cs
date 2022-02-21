using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    public class Bot
    {
        private readonly string _token;
        private readonly TelegramBotClient _bot;
        private BehaviourTree _behaviour;
        private string _chatId;

        public Bot(string token)
        {
            _token = token;
            _bot = new TelegramBotClient(_token);
        }

        public void StartChating()
        {
            _bot.StartReceiving();
            _bot.OnMessage += ReceiveMessage;
        }

        private void ReceiveMessage(object sender, MessageEventArgs arguments)
        {
            _chatId = arguments.Message.Chat.Id.ToString();
            string message = arguments.Message.Text;

            SendAnswer(message);
        }

        private void InitializeAll(string message)
        {
            if (_behaviour == null)
            {
                _behaviour = new BehaviourTree(new Dictionary<string, ICommand>
                {
                    { Messages.Start, new Start(_token, _chatId, message) },
                    { Messages.Stop, new Stop(_chatId) },
                    { Messages.Insert, new Insert(_chatId) }
                }); 
            }
        }

        private void SendAnswer(string message)
        {
            string answer = String.Empty;

            _bot.SendTextMessageAsync(_chatId, answer, Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }

        internal static Task SendTextMessageAsync(object id, string v1, bool v2, bool v3, int v4, ReplyKeyboardMarkup keyboard, ParseMode @default)
        {
            throw new NotImplementedException();
        }
    }
}
