using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PrimatScheduleBot
{
    public class Bot
    {
        private readonly string _token;
        private readonly TelegramBotClient _bot;
        private BehaviourTree _behaviour;

        public Bot(string token)
        {
            _token = token;
            _bot = new TelegramBotClient(_token);
            _behaviour = new BehaviourTree(new Dictionary<string, ICommand>
            {
                { Commands.Start, new Start(_token) },
                { Commands.Help, new Stop() }
            });
        }

        public void StartChating()
        {
            _bot.StartReceiving();
            _bot.OnMessage += ReceiveMessage;
        }

        private void ReceiveMessage(object sender, MessageEventArgs arguments)
        {
            var chatId = arguments.Message.Chat.Id.ToString();
            string message = arguments.Message.Text;

            if (_behaviour.StatesController.ContainsKey(message))
            {
                _behaviour.CurrentCommand = _behaviour.StatesController[message];
            }

            _behaviour.CurrentCommand.Execute(message, chatId);

            SendAnswer(message, chatId);
        }

        private void SendAnswer(string message, string chatId)
        {
            string answer = String.Empty;

            _bot.SendTextMessageAsync(chatId, answer, Telegram.Bot.Types.Enums.ParseMode.Markdown, null, true);
        }
    }
}
