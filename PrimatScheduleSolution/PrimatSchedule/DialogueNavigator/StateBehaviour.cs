using System.Collections.Generic;
using Telegram.Bot;

namespace PrimatScheduleBot
{
    public class StateBehaviour
    {
        public readonly Dictionary<string, ICommand> StateMachine;

        public StateBehaviour(Dictionary<string, ICommand> stateMachine) => StateMachine = stateMachine;
    }
}
