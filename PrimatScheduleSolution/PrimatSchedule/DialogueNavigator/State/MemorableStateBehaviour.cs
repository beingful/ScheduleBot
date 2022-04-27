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

        private void TrySetCurrentStateFromCache(string chatId)
        {
            ICommand state = _memory.Get(chatId);

            if (state != default(ICommand))
            {
                _stateBehaviour = new StateBehaviour(_stateBehaviour.StateMachine, state);
            }
        }

        public void TryChangeCurrentState(string chatId, string message)
        {
            TrySetCurrentStateFromCache(chatId);

            _stateBehaviour.TryChangeCurrentState(message);
        }

        public void SaveCurrentCommandInCache(string chatId) => _memory.Set(chatId, CurrentState);
    }
}