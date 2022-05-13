using System;
using System.Collections.Generic;
using System.Globalization;

namespace PrimatScheduleBot
{
    [Serializable]
    public class ChooseEvent<T> : ICommand where T : IPeriodicity, new()
    {
        private readonly DateTime _date;
        private ChooseEventUI _ui;
        private List<Event> _schedule;
        private ICommand _currentState;

        public ChooseEvent(string chatId, DateTime date) 
        {
            _date = date;
            SetProperties(chatId);
        }

        private void SetProperties(string chatId)
        {
            SetSchedule(chatId);
            SetUI();
        }

        private void SetSchedule(string chatId)
        {
            var schedule = new ScheduleGetter(chatId, _date);

            _schedule = schedule.Get();
        }

        private void SetUI() => _ui = new ChooseEventUI(_schedule);

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (_ui.DoesUIExist(info.LastMessage))
            {
                SetProperties(info.ChatId);

                ui = _ui.GetUI(info.LastMessage);
            }
            else
            {
                TryChangeCurrentCommand(info.LastMessage);

                ui = _currentState.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(string message)
        {
            int index;

            if (IsCorrectNumber(message, out index))
            {
                Event selectedEvent = GetEvent(index);

                _currentState = GetCommand(message, selectedEvent);
            }
            else
            {
                Validation.NotNull(_currentState);
            }
        }

        private bool IsCorrectNumber(string message, out int index)
            => Int32.TryParse(message, NumberStyles.Integer, null, out index) && index <= _schedule.Count;

        private Event GetEvent(int index)
        {
            Guid selectedEventId = _schedule[index - 1].Id;

            using var facade = new EventFacade();

            CheckDoesEventExist(selectedEventId, facade);

            return facade.GetById(selectedEventId);
        }

        private void CheckDoesEventExist(Guid selectedEventId, EventFacade facade)
        {
            bool doesEventExist = facade.DoesAnyEventExist(@event => @event.Id == selectedEventId);

            Validation.ValidateDoesEventExist(doesEventExist);
        }

        private ICommand GetCommand(string message, Event @event)
        {
            return new Command(new UIBehaviour(new Dictionary<string, UI>
            {
                { message, new UI("Що ви хочете зробити з цією подією?",
                new List<string> { Buttons.Delete, Buttons.Update }, 2) }
            }), new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Buttons.Update, new Update<T>(@event.Id) },
                { Buttons.Delete, new Delete(@event.Id) }
            }));
        }
    }
}