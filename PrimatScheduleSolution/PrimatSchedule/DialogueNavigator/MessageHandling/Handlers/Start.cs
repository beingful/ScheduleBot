using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Start : ICommand, IHandler
    {
        private readonly string _token;
        private readonly string _chatId;
        private readonly string _message;
        private const int _statesCount = 2;
        private DialogueState _dialogue = new DialogueState();
        private readonly Dictionary<DialogueState.States, Func<string>> _dialogueNavigator;

        public Start(string token, string chatId, string message)
        {
            _token = token;
            _chatId = chatId;
            _message = message;

            _dialogueNavigator = new Dictionary<DialogueState.States, Func<string>>
            {
                { DialogueState.States.Default, () => AskForTime() },
                { DialogueState.States.First, () => StartMailingList() }
            };
        }

        public string Execute(string message)
        {
            string answer = _dialogueNavigator[_dialogue.CurrentState]();
            _dialogue.ChangeState(_statesCount);

            return answer;
        }

        private string AskForTime() => "О котрій годині ви хочете отримувати сповіщення?";

        private string StartMailingList()
        {
            DateTime time = MessageValidator.TryGetDateTime(_message);
            PostScheduler.TryStart(_token, _chatId, time);

            return $"Ви підписалися на щоденну розсилку розкладу о {time}.";
        }

        public void HandleReplyButton(string message)
        {
            throw new NotImplementedException();
        }
    }
}
