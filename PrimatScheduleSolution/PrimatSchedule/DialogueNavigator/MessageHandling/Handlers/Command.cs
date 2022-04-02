using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class Command : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private readonly StateBehaviour _stateBehaviour;
        private ICommand _currentCommand;

        public Command(UIBehaviour uiBehaviour, StateBehaviour stateBehaviour)
        {
            _uiBehaviour = uiBehaviour;
            _stateBehaviour = stateBehaviour;
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            try
            {
                ui = _uiBehaviour.StateMachine[info.LastMessage];
                //_currentCommand = null;
            }
            catch
            {
                TryChangeCurrentCommand(info.LastMessage);

                ui = ExecuteNext(info);
            }

            return ui;
        }

        private UI ExecuteNext(ChatInfo info)
        {
            try
            {
                return _currentCommand.Execute(info);
            }
            catch (NullReferenceException)
            {
                throw new IncorrectMessageException();
            }
        }

        private void TryChangeCurrentCommand(string message)
        {
            _currentCommand = _stateBehaviour
                                    .StateMachine
                                    .GetValueOrDefault(message) ?? _currentCommand;
        }
    }
}
