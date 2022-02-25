using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class Edit : ICommand, IHandler
    {
        private readonly string _chatId;
        private readonly BehaviourTree _tree;

        public Edit(string chatId, BehaviourTree tree)
        {
            _chatId = chatId;
            _tree = tree;
        }

        public string Execute()
        {
            throw new NotImplementedException();
        }

        public void HandleReplyButton(string message)
        {
            throw new NotImplementedException();
        }
    }
}
