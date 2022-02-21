using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;

namespace PrimatScheduleBot
{
    public class UIBehaviour
    {
        public readonly TelegramBotClient _bot;
        private static long _chatId;
        private readonly string _message;
        public readonly Dictionary<string, Action> OnClickButtonHandlers;

        public UIBehaviour(TelegramBotClient bot, long chatId, string message, Dictionary<string, Action> onClickButtonHandlers) 
            : this(onClickButtonHandlers)
        {
            _bot = bot;
            _chatId = chatId;
            _message = message;
        }

        public UIBehaviour(Dictionary<string, Action> onClickButtonHandlers)
        {
            OnClickButtonHandlers = onClickButtonHandlers;
        }

        public async void DrawButtons()
        {
            var buttons = new List<List<KeyboardButton>>();
            List<string> buttonsCaption = OnClickButtonHandlers.Keys.ToList();

            for (int i = 0; i < buttonsCaption.Count; i+=2)
            {
                buttons.Add(new List<KeyboardButton>
                {
                    new KeyboardButton(buttonsCaption[i]),
                    new KeyboardButton(buttonsCaption[i+1])
                });
            }

            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = buttons,
                ResizeKeyboard = true
            };

            await _bot.SendTextMessageAsync(_chatId, _message, Telegram.Bot.Types.Enums.ParseMode.Default, null, false, false, 0, false, keyboard);
        }
    }
}
