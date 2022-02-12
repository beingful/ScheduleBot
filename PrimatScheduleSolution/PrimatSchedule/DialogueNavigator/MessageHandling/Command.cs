using System.Collections.Generic;

namespace PrimatScheduleBot
{
    internal abstract class Command
    {
        protected readonly Dictionary<MessageResult, string> Messages;

        public Command(Dictionary<MessageResult, string> messages) => Messages = messages;

        public abstract string HandleAndSendAnswer();
    }
}
