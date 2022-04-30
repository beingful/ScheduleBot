using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class ChooseEvent : ICommand
    {
        public readonly UIBehaviour _uiBehaviour;
        private readonly List<Event> _schedule;
        private readonly IPeriodicity _period;
        private ICommand _currentCommand;
        private UI _ui;

        public ChooseEvent(List<Event> schedule, IPeriodicity period)
        {
            _schedule = schedule;
            _period = period;
            _ui = GetUI();
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI> { { Buttons.Event, _ui } });
        }

        private UI GetUI()
        {
            var buttons = new List<string>();

            for (int i = 0; i < _schedule.Count; i++)
            {
                buttons.Add((i + 1).ToString());
            }

            var parser = new ScheduleToMessage(_schedule);

            return new UI(parser.Convert(true), buttons);
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
                TryChangeCurrentCommand(info.LastMessage);

                ui = _currentCommand.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(string message)
        {
            if (_ui.ButtonCaptions.Contains(message))
            {
                Event selectedEvent = GetEvent(message);

                _currentCommand = GetCommand(message, selectedEvent);
            }
            else
            {
                MessageValidator.ValidateMessage(_currentCommand != null);
            }
        }

        private Event GetEvent(string message)
        {
            int index = Convert.ToInt32(message);
            Guid selectedEventId = _schedule[index - 1].Id;

            using var facade = new EventFacade();

            CheckDoesEventExist(selectedEventId, facade);

            return facade.GetById(selectedEventId);
        }

        private void CheckDoesEventExist(Guid selectedEventId, EventFacade facade)
        {
            bool doesEventExist = facade.DoesAnyEventExist(@event => @event.Id == selectedEventId);

            MessageValidator.ValidateDoesEventExist(doesEventExist);
        }

        private ICommand GetCommand(string message, Event @event)
        {
            return new Command(new UIBehaviour(new Dictionary<string, UI>
            {
                { message, new UI("Що ви хочете зробити з цією подією?",
                new List<string> { Buttons.Delete, Buttons.Update }) }
            }), new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Buttons.Update, new Update(@event, _period) },
                { Buttons.Delete, new Delete(@event.Id) }
            }));
        }
    }
}