using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Insert<T> : ICommand where T : IPeriodicity, new()
    {
        private readonly UIBehaviour _uiBehaviour;

        public Insert()
        {
            var period = new T();

            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { period.Name, GetUI(period.Name) }
            });
        }

        private UI GetUI(string required)
        {
            var display = new PropertiesDisplay<T>();

            string message = "Так тримати! Погнали далі!\n" +
                "Заповни і надійшли мені дані про подію в наступному форматі (графа "
                + $"{ required } обов'язкова для заповнення):\n" +
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

                InsertEvent(@event);

                ui = new UI("Я додав подію в твій розклад.", Stickers.Done);
            }

            return ui;
        }

        private Event GetNewEvent(ChatInfo info)
        {
            var converter = new MessageToEvent<T>(info.ChatId, info.LastMessage);

            return converter.Convert();
        }

        private void InsertEvent(Event @event)
        {
            using var facade = new EventFacade();

            facade.Insert(@event);
        }
    }
}