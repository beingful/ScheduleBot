using System.Collections.Generic;

namespace PrimatScheduleBot
{
    abstract class Command
    {
        protected readonly Dictionary<MessageResult, string> Messages;

        public Command(Dictionary<MessageResult, string> messages) => Messages = messages;

        public abstract string DoTaskAndGetMessage();
    }
}
