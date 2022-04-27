using System;

namespace PrimatScheduleBot
{
    public class Calendar<T> : ICommand where T : ICalendarDay, new()
    {
        private readonly UIBehaviour _uiBehaviour;
        private ICommand _currentSate;

        public Calendar(UIBehaviour uiBehaviour) => _uiBehaviour = uiBehaviour;

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (_uiBehaviour.IsSuchAKeyExist(info.LastMessage))
            {
                ui = _uiBehaviour.GetUI(info.LastMessage);

                ClearCurrentState();
            }
            else
            {
                if (_currentSate is null)
                {
                    TryChangeCurrentState(info);
                }
                
                ui = _currentSate.Execute(info);
            }

            return ui;
        }

        private void ClearCurrentState() => _currentSate = default(ICommand);

        private void TryChangeCurrentState(ChatInfo info)
        {
            var calendarDay = new T();

            DateTime date = calendarDay.TryGetDate(info.LastMessage);

            _currentSate = new ChooseSchedule(info.ChatId, date, calendarDay as IPeriodicity);
        }
    }
}