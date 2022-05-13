using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class ChooseEventUI
    {
        private readonly List<Event> _schedule;
        private readonly UIBehaviour _uiBehaviour;

        public ChooseEventUI(List<Event> schedule)
        {
            _schedule = schedule;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI> { { Buttons.Event, GetUI() } });
        }

        private UI GetUI() => new UI(GetMessage(), GetButtons(), GetButtonsInTheRow());

        private List<string> GetButtons()
        {
            var buttons = new List<string>();

            for (int i = 0; i < _schedule.Count; i++)
            {
                buttons.Add($"{i + 1}");
            }

            return buttons;
        }

        private int GetButtonsInTheRow()
        {
            int number;
            int maxNumber = 3;

            if (_schedule.Count < maxNumber)
            {
                number = _schedule.Count % maxNumber;
            }
            else
            {
                number = maxNumber;
            }

            return number;
        }

        private string GetMessage()
        {
            var parser = new ScheduleToMessage(_schedule);

            return parser.Convert(true);
        }

        public bool DoesUIExist(string message) => _uiBehaviour.IsSuchAKeyExist(message);

        public UI GetUI(string message) => _uiBehaviour.GetUI(message);
    }
}