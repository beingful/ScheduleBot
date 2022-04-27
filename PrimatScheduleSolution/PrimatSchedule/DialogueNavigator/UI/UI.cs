using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    public class UI
    {
        public readonly string Question;
        public readonly string StickerId;
        public readonly List<string> ButtonCaptions;

        public UI(string question, List<string> buttonCaptions = null) 
        {
            Question = question;
            ButtonCaptions = buttonCaptions;
        }

        public UI(string question, string stickerId, List<string> buttonCaptions = null) : this(question, buttonCaptions)
        {
            StickerId = stickerId;
        }

        public InlineKeyboardMarkup GetInlineKeyboard()
        {
            var keyboard = GetKeyboard();

            return new InlineKeyboardMarkup(keyboard);
        }

        private IEnumerable<IEnumerable<InlineKeyboardButton>> GetKeyboard()
        {
            var keyboard = new List<List<InlineKeyboardButton>>();

            for (int i = 0; i < ButtonCaptions?.Count; i++)
            {
                var keyBoardButtons = new List<InlineKeyboardButton>();

                keyBoardButtons.Add(InlineKeyboardButton.WithCallbackData(ButtonCaptions[i]));

                keyboard.Add(keyBoardButtons);
            }

            return keyboard;
        }
    }
}
