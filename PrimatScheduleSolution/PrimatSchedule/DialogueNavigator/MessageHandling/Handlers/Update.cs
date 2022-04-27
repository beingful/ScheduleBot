using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Update : ICommand
    {
        private readonly Event _event;
        private readonly UIBehaviour _uiBehaviour;
        private readonly IPeriodicity _period;

        public Update(Event @event, IPeriodicity period)
        {
            _event = @event;
            _period = period;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI> 
            { 
                { Buttons.Update, GetUI(@event, period) } 
            });
        }

        private UI GetUI(Event @event, IPeriodicity period)
        {
            var parser = new EventToMessage(@event);

            string message = "Внесіть корективи і надішліть мені наступний шаблон:\n\n" 
                + $"`{ parser.ParseAll(period) }`";

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

                MessageValidator.ValidateTimeForDuplications(@event);

                UpdateEvent(@event);

                ui = new UI("Я змінив подію в розкладі.", Stickers.Done);
            }

            return ui;
        }

        private Event GetModifiedEvent(string message)
        {
            var converter = new MessageToEvent(message, _period, _event);

            return converter.Convert();
        }

        private void UpdateEvent(Event @event)
        {
            using var facade = new EventFacade();

            facade.Update(@event);
        }
    }
}