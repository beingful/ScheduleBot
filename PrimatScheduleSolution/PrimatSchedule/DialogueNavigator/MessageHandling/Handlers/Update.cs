using System;

namespace PrimatScheduleBot
{
    public sealed class Update : ICommand, IHandler
    {
        private readonly BehaviourTree _tree;
        private readonly string _chatId;

        public Update(string chatId) => _chatId = chatId;

        public string Execute(string message)
        {
            throw new NotImplementedException();
        }

        public void HandleReplyButton(string message)
        {
            throw new NotImplementedException();
        }
    }
}
