using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatScheduleBot.DialogueNavigator.MessageHandling.Handlers
{
    class StartDialogue : ICommand, IHandler
    {
        private readonly string _chatId;
        private readonly string _message;
        private readonly BehaviourTree _tree;

        public StartDialogue(string chatId, string message, BehaviourTree tree)
        {
            _chatId = chatId;
            _message = message;
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
