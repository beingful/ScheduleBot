using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Update<T> : ICommand where T : IPeriodicity, new()
    {
        private readonly Event _event;
        private readonly UIBehaviour _uiBehaviour;

        public Update(Event @event)
        {
            _event = @event;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI> 
            { 
                { Buttons.Update, GetUI(@event) } 
            });
        }

        private UI GetUI(Event @event)
        {
            var parser = new EventToMessage(@event);

            string message = "Внесіть корективи і надішліть мені наступний шаблон:\n\n" 
                + $"`{ parser.ParseAll<T>() }`";

            return new UI(message);
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