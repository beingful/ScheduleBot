using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Insert : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private readonly Func<string, Event, int> _insertEvent;

        public Insert(Func<string, Event, int> insertEvent)
        {
            _insertEvent = insertEvent;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Date, GetUI(Buttons.Date, nameof(Buttons.Day)) },
                { Buttons.Day, GetUI(Buttons.Day, nameof(Buttons.Date)) }
            });
        }

        private UI GetUI(string required, string excluded)
        {
            var template = new Template();

            string message = "Так тримати! Погнали далі!\nЗаповни і надійшли мені дані про подію в наступному форматі (графа "
                + $"{required.ToLower()}"
                + " обов'язкова для заповнення):\n\n";

            message += $"`{template.ToMessageAllExcluded(excluded)}`";

            return new UI(message, Stickers.Freedom);
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            try
            {
                ui = _uiBehaviour.StateMachine[info.LastMessage];
            }
            catch
            {
                var result = new CommandResult();
                var converter = new MessageToEvent(info.LastMessage, new Event());

                Event newEvent = converter.Parse();

                int resultIndex = _insertEvent.Invoke(info.ChatId, newEvent);

                ui = result._results[resultIndex];
            }

            return ui;
        }
    }
}
