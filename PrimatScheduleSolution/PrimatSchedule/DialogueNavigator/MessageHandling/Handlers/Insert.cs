using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Insert : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private readonly IPeriodicity _period;

        public Insert(string button, IPeriodicity period)
        {
            _period = period;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { button, GetUI(period.Name) }
            });
        }

        private UI GetUI(string required)
        {
            var display = new PropertiesDisplay(_period);

            string message = "Так тримати! Погнали далі!\n" +
                "Заповни і надійшли мені дані про подію в наступному форматі (графа "
                + $"{ required } обов'язкова для заповнення):\n\n" +
                $"`{ display }`";

            return new UI(message, Stickers.Freedom);
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (_uiBehaviour.IsSuchAKeyExist(info.LastMessage))
            {
                ui = _uiBehaviour.GetUI(info.LastMessage);
            }
            else
            {
                Event @event = GetNewEvent(info);

                MessageValidator.ValidateTimeForDuplications(@event);

                InsertEvent(@event);

                ui = new UI("Я додав подію в твій розклад.", Stickers.Done);
            }

            return ui;
        }

        private Event GetNewEvent(ChatInfo info)
        {
            var converter = new MessageToEvent(info.ChatId, info.LastMessage, _period);

            return converter.Convert();
        }

        private void InsertEvent(Event @event)
        {
            using var facade = new EventFacade();

            facade.Insert(@event);
        }
    }
}