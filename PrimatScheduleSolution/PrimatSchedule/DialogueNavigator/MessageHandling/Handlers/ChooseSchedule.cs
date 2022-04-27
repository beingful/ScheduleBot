using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class ChooseSchedule : ICommand
    {
        private readonly ICommand _currentState;
        private readonly List<Event> _schedule;

        public ChooseSchedule(string chatId, DateTime date, IPeriodicity period) 
        {
            _currentState = new Command(new UIBehaviour(new Dictionary<string, UI>
            {
                { date.ToString(), new UI("Що робимо далі?", new List<string> { Buttons.Get, Buttons.Event }) }
            }), new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Buttons.Get, new Get(_schedule) },
                { Buttons.Event, new ChooseEvent(_schedule, period) }
            }));

            _schedule = GetSchedule(chatId, date);
        }

        private List<Event> GetSchedule(string chatId, DateTime date)
        {
            var facade = new EventFacade();

            return facade.GetByDate(chatId, date).ToList();
        }

        public UI Execute(ChatInfo info) 
        {
            MessageValidator.ValidateIsScheduleEmpty(_schedule.Count);

            return _currentState.Execute(info);
        }
    }
}