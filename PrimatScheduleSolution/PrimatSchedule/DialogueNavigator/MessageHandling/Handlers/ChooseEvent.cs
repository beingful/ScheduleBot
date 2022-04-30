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
                if (_currentCommand is null)
                {
                    TryChangeCurrentCommand(info.LastMessage);
                }

                ui = _currentCommand.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(string message)
        {
            MessageValidator.ValidateMessage(_ui.ButtonCaptions.Contains(message));

            int index = Convert.ToInt32(message);
            Event selectedEvent = _schedule[index - 1];

            _currentCommand = GetCommand(message, selectedEvent);
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