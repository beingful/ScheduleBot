using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class StateBehaviour
    {
        public readonly Dictionary<string, ICommand> StateMachine;

        public StateBehaviour(Dictionary<string, ICommand> stateMachine) => StateMachine = stateMachine;

        public StateBehaviour(Dictionary<string, ICommand> stateMachine, ICommand currentState) : this(stateMachine)
            => CurrentState = currentState;

        public ICommand CurrentState { get; private set; }

        public ICommand /*void*/ TryChangeCurrentState(string message, ICommand curcommand)
        {
            ICommand command = curcommand;

            if (StateMachine.ContainsKey(message))
            {
                command = StateMachine[message];
                //CurrentState = StateMachine[message];
            }
            else
            {
                MessageValidator.ValidateMessage(command != null);
            }

            return command;
        }
    }
}