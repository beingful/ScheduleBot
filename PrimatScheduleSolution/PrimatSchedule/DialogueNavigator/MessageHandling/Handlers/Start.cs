using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Start : ICommand, IHandler
    {
        private readonly string _token;
        private const int _statesCount = 2;
        private DialogueState _dialogue;
        private readonly Dictionary<DialogueState.States, Func<string, string, string>> _dialogueNavigator;

        public Start(string token)
        {
            _dialogue = new DialogueState();

            _dialogueNavigator = new Dictionary<DialogueState.States, Func<string, string, string>>
            {
                { DialogueState.States.Default, (message, chatId) => AskForTime() },
                { DialogueState.States.First, (message, chatId) => StartMailingList(message, chatId) }
            };

            _token = token;
        }

        public string Execute()
        {
            string answer = _dialogueNavigator[_dialogue.CurrentState](message, chatId);
            _dialogue.ChangeState(_statesCount);

            return answer;
        }

        private string AskForTime() => "О котрій годині ви хочете отримувати сповіщення?";

        private string StartMailingList(string message, string chatId)
        {
            DateTime time = MessageValidator.TryGetDateTime(message);
            PostScheduler.TryStart(_token, chatId, time);

            return $"Ви підписалися на щоденну розсилку розкладу о {time}.";
        }

        public void HandleReplyButton(string message)
        {
            throw new NotImplementedException();
        }
    }
}
