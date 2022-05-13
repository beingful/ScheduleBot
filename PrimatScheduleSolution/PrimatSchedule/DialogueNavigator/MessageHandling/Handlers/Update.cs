using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Update<T> : ICommand where T : IPeriodicity, new()
    {
        private readonly Guid _eventId;
        private Event _event;

        public Update(Guid eventId) => _eventId = eventId;

        private UI GetUI()
        {
            var parser = new EventToMessage(_event);

            string message = "Внесіть корективи і надішліть мені наступний шаблон:\n\n" 
                + $"`{ parser.ParseAll<T>() }`";

            return new UI(message);
        }

        private Event TryGetEvent()
        {
            var getter = new EventGetter(_eventId);

            return getter.TryGetEvent();
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (info.LastMessage is Buttons.Update)
            {
                _event = TryGetEvent();

                ui = GetUI();
            }
            else
            {
                Event @event = GetModifiedEvent(info.LastMessage);

                UpdateEvent(@event);

                ui = new UI("Я змінив подію в розкладі.", Stickers.Done);
            }

            return ui;
        }

        private Event GetModifiedEvent(string message)
        {
            var converter = new MessageToEvent<T>(message, _event);

            return converter.Convert();
        }

        private void UpdateEvent(Event @event)
        {
            using var facade = new EventFacade();

            facade.Update(@event);
        }
    }
}