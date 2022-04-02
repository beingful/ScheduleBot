using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class OnDay : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private ICommand _currentCommand;

        [OnDay]
        public string Day { get; private set; }

        public OnDay()
        {
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Day, new UI("Введи день тижня, щоб я зміг знайти твій розклад.") }
            });
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            try
            {
                ui = _uiBehaviour.StateMachine[info.LastMessage];
                _currentCommand = null;
            }
            catch
            {
                TryChangeCurrentCommand(info);

                ui = _currentCommand.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(ChatInfo info)
        {
            Day = info.LastMessage;

            bool isDay = IsValid();

            if (isDay)
            {
                _currentCommand = new ChooseSchedule(info.ChatId, info.LastMessage, 
                    Querier.GetScheduleByDay, Querier.UpdateEventOnDay);
            }
            else if (_currentCommand is null)
            {
                throw new DayFormatException();
            }
        }

        public bool IsValid()
        {
            var context = new ValidationContext(this) { MemberName = nameof(Day) };

            return Validator.TryValidateProperty(Day, context, null);
        }
    }
}
