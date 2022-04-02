using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class ChooseSchedule : ICommand
    {
        private readonly Schedule _schedule;
        private readonly ICommand _currentCommand;

        public ChooseSchedule(string chatId, string dayOrDate, 
            Func<string, string, Schedule> getEvent, Func<string, Event, int> updateEvent) 
        {
            _schedule = getEvent(chatId, dayOrDate);

            _currentCommand = new Command(new UIBehaviour(new Dictionary<string, UI>
            {
                { dayOrDate.ToString(), new UI("Що робимо далі?", new List<string> { Buttons.Get, Buttons.Event }) }
            }), new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Buttons.Get, new Get(_schedule) },
                { Buttons.Event, new ChooseEvent(_schedule, updateEvent) }
            }));
        }

        public UI Execute(ChatInfo info) 
        {
            if (_schedule.Events.Count < 0)
            {
                throw new NoOneScheduleException();
            }

            return _currentCommand.Execute(info);
        }
    }
}
