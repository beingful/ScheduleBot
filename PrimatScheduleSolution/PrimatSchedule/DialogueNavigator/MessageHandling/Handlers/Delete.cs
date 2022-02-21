using System;

namespace PrimatScheduleBot
{
    public sealed class Delete : ICommand, IHandler
    {
        private readonly string _chatId;

        public Delete(string chatId) => _chatId = chatId;

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
