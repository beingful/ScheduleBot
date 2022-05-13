using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Calendar<T> : ICommand where T : IPeriodicity, new()
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

            DateTime date = calendarDay.CalculateDate(info.LastMessage);

            _currentSate = new Command(new UIBehaviour(new Dictionary<string, UI>
            {
                { info.LastMessage, new UI("Що робимо далі?", 
                new List<string> { Buttons.Get, Buttons.Event }, 2) }
            }), new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Buttons.Get, new Get(date) },
                { Buttons.Event, new ChooseEvent<T>(info.ChatId, date) }
            }));
        }
    }
}