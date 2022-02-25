using System;
using System.Collections.Generic;
using Telegram.Bot.Args;

namespace PrimatScheduleBot
{
    public class BehaviourTree
    {
        public event Action OnCurrentStateChange;
        public readonly UIBehaviour UI;
        public readonly Dictionary<string, ICommand> StatesController;
        private IHandler _currentHandler;
        private ICommand _currentCommand;

        public ICommand CurrentCommand
        {
            get => _currentCommand;
            set
            {
                _currentCommand = value;
                _currentHandler = _currentCommand as IHandler;

                if (_currentHandler != null)
                {
                    OnCurrentStateChange?.Invoke();
                }
            }
        } 

        private BehaviourTree(Dictionary<string, ICommand> statesController)
        {
            StatesController = statesController;
            OnCurrentStateChange += SendButtons;
        }

        public BehaviourTree(Dictionary<string, ICommand> statesController, UIBehaviour uI) : this(statesController)
        {
            UI = uI;
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

            _currentHandler.HandleReplyButton(message);
        }
    }
}
