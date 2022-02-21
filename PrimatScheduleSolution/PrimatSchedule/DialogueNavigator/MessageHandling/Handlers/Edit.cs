using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class Edit : ICommand, IHandler
    {
        private readonly BehaviourTree _tree;
        private readonly string _chatId;
        private readonly string _message;

        public Edit(string chatId) 
        {
            _tree = new BehaviourTree(new Dictionary<string, ICommand>
            {
                { Messages.Insert, new Insert(_chatId) },
                { Messages.Update, new Update(_chatId) },
                { Messages.Delete, new Delete(_chatId) }
            });
            _chatId = chatId;
        }

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
