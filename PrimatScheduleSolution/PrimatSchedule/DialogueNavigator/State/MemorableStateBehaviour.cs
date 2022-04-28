using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class MemorableStateBehaviour
    {
        private readonly DialogueMemory _memory;
        private StateBehaviour _stateBehaviour;

        public MemorableStateBehaviour(Dictionary<string, ICommand> stateMachine)
        {
            _stateBehaviour = new StateBehaviour(stateMachine);
            _memory = new DialogueMemory();
        }

        public ICommand CurrentState { get => _stateBehaviour.CurrentState; }

        private ICommand TrySetCurrentStateFromCache(string chatId)
        {
            ICommand state = _memory.Get(chatId);

            //_stateBehaviour = new StateBehaviour(_stateBehaviour.StateMachine, state);

            //return state;

            return state;
        }

        public ICommand /*void*/ TryChangeCurrentState(string chatId, string message)
        {
            ICommand command = TrySetCurrentStateFromCache(chatId);

            command = _stateBehaviour.TryChangeCurrentState(message, command);

            return command;
        }

        public void SaveCurrentCommandInCache(string chatId, ICommand currentState) => _memory.Set(chatId, currentState);
    }
}