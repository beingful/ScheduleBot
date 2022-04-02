using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class OnDate : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private ICommand _currentCommand;

        public OnDate()
        {
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Date, new UI("Введи дату, щоб я зміг знайти твій розклад.") }
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
            bool formatResult = IsValidValue(new OnDateAttribute(), info.LastMessage);

            if (formatResult)
            {
                //var date = DateTime.Parse(info.LastMessage);

                //bool boundsResult = IsValidValue(new OnDateBoundsAttribute(), date);

                //if (boundsResult)
                //{
                    _currentCommand = new ChooseSchedule(info.ChatId, info.LastMessage,
                        Querier.GetScheduleByDate, Querier.UpdateEventOnDate);
                //}
                //else if (_currentCommand is null)
                //{
                //    throw new TooEarlyDateException();
                //}
            }
            else if (_currentCommand is null)
            {
                throw new DateFormatException();
            }
        }

        private bool IsValidValue<T>(ValidationAttribute attribute, T value)
        {
            var context = new ValidationContext(this);
            var attributeToCheckValue = new List<ValidationAttribute> { attribute };

            return Validator.TryValidateValue(value, context, null, attributeToCheckValue);
        }
    }
}
