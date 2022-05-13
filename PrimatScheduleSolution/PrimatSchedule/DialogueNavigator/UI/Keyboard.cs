using System;
using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Keyboard
    {
        private readonly List<string> _buttonCaptions;
        private readonly int _buttonsInTheRow;

        public Keyboard(List<string> buttonCaptions, int buttonsInTheRow)
        {
            _buttonCaptions = buttonCaptions;
            _buttonsInTheRow = buttonsInTheRow;
        }

        public InlineKeyboardMarkup Get()
        {
            var keyboard = new List<List<InlineKeyboardButton>>();
            int? buttonsCount = _buttonCaptions?.Count;

            for (int i = 0; i < buttonsCount; i += _buttonsInTheRow)
            {
                var keyBoardButtons = new List<InlineKeyboardButton>();

                int? inRow = i + _buttonsInTheRow;

                if (inRow > buttonsCount)
                {
                    inRow = buttonsCount;
                }

                for (int j = i; j < inRow; j++)
                {
                    keyBoardButtons.Add(InlineKeyboardButton.WithCallbackData(_buttonCaptions[j]));
                }

                keyboard.Add(keyBoardButtons);
            }

            return new InlineKeyboardMarkup(keyboard);
        }
    }
}
