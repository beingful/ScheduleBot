using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace PrimatScheduleBot
{
    public class BehaviourTree
    {
        public event Action OnCurrentStateChange;
        public readonly UIBehaviour UI;
        public readonly Dictionary<string, ICommand> StatesController;
        private IHandler _currentState;
        public IHandler CurrentState 
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnCurrentStateChange?.Invoke();
            } 
        }

        private BehaviourTree(Dictionary<string, ICommand> statesController)
        {
            StatesController = statesController;
            OnCurrentStateChange += SendButtons;
        }

        public BehaviourTree(Dictionary<string, ICommand> statesController, TelegramBotClient bot, 
            long chatId, string message, Dictionary<string, Action> onClickButtonHandlers = null) : this(statesController)
        {
            UI = new UIBehaviour(bot, chatId, message, onClickButtonHandlers);
        }

        private void SendButtons()
        {
            if (UI.OnClickButtonHandlers != null)
            {
                UI._bot.OnCallbackQuery += OnReplyButtonClick;
                UI.DrawButtons();
            }
        }

        private void OnReplyButtonClick(object sc, CallbackQueryEventArgs ev)
        {
            string message = ev.CallbackQuery.Message.Text;

            _currentState.HandleReplyButton(message);
        }

        public BehaviourTree(Dictionary<string, ICommand> statesController, Dictionary<string, Action> onClickButtonHandlers = null) : this(statesController)
        {
            UI = new UIBehaviour(onClickButtonHandlers);
        }
    }
}
