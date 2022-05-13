using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    [Serializable]
    public class UI
    {
        public readonly string Question;
        public readonly string StickerId;
        private readonly Keyboard _keyboard;

        public UI(string question) => Question = question;

        public UI(string question, string stickerId) : this(question) => StickerId = stickerId;

        public UI(string question, List<string> buttonCaptions, int buttonsInTheRow) : this(question)
            => _keyboard = new Keyboard(buttonCaptions, buttonsInTheRow);

        public UI(string question, List<string> buttonCaptions, int buttonsInTheRow, string stickerId)
            : this(question, buttonCaptions, buttonsInTheRow) => StickerId = stickerId;

        public InlineKeyboardMarkup GetInlineKeyboard() => _keyboard?.Get();
    }
}
