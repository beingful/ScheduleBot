using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class ChooseEvent : ICommand
    {
        public readonly UIBehaviour _uiBehaviour;
        private readonly Schedule _schedule;
        private readonly Func<string, Event, int> _updateEvent;
        private ICommand _currentCommand;

        public ChooseEvent(Schedule schedule, Func<string, Event, int> updateEvent)
        {
            _schedule = schedule;
            _updateEvent = updateEvent;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Event, GetUI() }
            });
        }

        private UI GetUI()
        {
            var buttons = new List<string>();

            for (int i = 0; i < _schedule.Events.Count; i++)
            {
                buttons.Add((i + 1).ToString());
            }

            return new UI(_schedule.ToString(), buttons);
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            try
            {
                ui = _uiBehaviour.StateMachine[info.LastMessage];
            }
            catch (KeyNotFoundException)
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
            try
            {
                int index = Convert.ToInt32(message);
                Event selectedEvent = _schedule[index];

                _currentCommand = new Command(new UIBehaviour(new Dictionary<string, UI>
                {
                    { message, new UI("Що ви хочете зробити з цією подією?", new List<string>
                    { Buttons.Get, Buttons.Delete, Buttons.Update }) }
                }), new StateBehaviour(new Dictionary<string, ICommand>
                {
                    { Buttons.Update, new Update(selectedEvent, _updateEvent) },
                    { Buttons.Delete, new Delete(selectedEvent.Id) },
                    { Buttons.Get, new Delete(selectedEvent.Id) }
                }));
            }
            catch
            {
                throw new IncorrectMessageException();
            }
        }
    }
}
