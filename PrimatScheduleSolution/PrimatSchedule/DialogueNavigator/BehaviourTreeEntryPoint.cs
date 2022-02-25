using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace PrimatScheduleBot
{
    public class BehaviourTreeEntryPoint
    {
        private readonly string _token;
        private readonly string _chatId;
        private readonly string _message;
        private readonly TelegramBotClient _bot;

        public BehaviourTreeEntryPoint(string token, string chatId, string message, TelegramBotClient bot)
        {
            _token = token;
            _chatId = chatId;
            _message = message;
            _bot = bot;
        }

        public BehaviourTree Create()
        {
            return new BehaviourTree(new Dictionary<string, ICommand>
            {
                { Commands.Start, new Start(_token, new BehaviourTree(new Dictionary<string, ICommand> 
                { 
                })) },
                { Commands.Help, new Stop() }
            });
        }
    }
}
