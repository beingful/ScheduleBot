using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrimatScheduleBot
{
    public sealed class Update : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private readonly Event _event;
        private readonly Func<string, Event, int> _updateEvent;

        public Update(Event @event, Func<string, Event, int> updateEvent)
        {
            _event = @event;
            _updateEvent = updateEvent;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Update, GetUI(@event) }
            });
        }

        private UI GetUI(Event @event)
        {
            var parser = new EventToMessage(@event);

            string message = "Внесіть корективи і надішліть мені наступний шаблон:\n\n";

            message += parser.ParseAll();

            return new UI(message);
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

                var updating = Task.Run(() => _updateEvent(info.ChatId, _event));

                int resultIndex = updating.Result;

                ui = result._results[resultIndex];
            }

            return ui;
        }
    }
}
