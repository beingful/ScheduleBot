using System;

namespace PrimatScheduleBot
{
    public sealed class Insert : ICommand, IHandler
    {
        private readonly BehaviourTree _tree;

        public string Execute(string message, string chatId)
        {
            throw new NotImplementedException();
        }

        public void HandleReplyButton(string message)
        {
            throw new NotImplementedException();
        }
    }
}
