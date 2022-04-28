using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class StateBehaviour
    {
        public readonly Dictionary<string, ICommand> StateMachine;

        public StateBehaviour(Dictionary<string, ICommand> stateMachine) => StateMachine = stateMachine;

        public StateBehaviour(Dictionary<string, ICommand> stateMachine, ICommand currentState) : this(stateMachine)
            => CurrentState = currentState;

        public ICommand CurrentState { get; private set; }

        public void TryChangeCurrentState(string message)
        {
            if (StateMachine.ContainsKey(message))
            {
                CurrentState = StateMachine[message].DeepClone();
            }
            else
            {
                MessageValidator.ValidateMessage(CurrentState != null);
            }
        }
    }
}