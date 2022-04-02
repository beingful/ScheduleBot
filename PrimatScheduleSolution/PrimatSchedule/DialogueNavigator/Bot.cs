using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    public class Bot
    {
        private readonly TelegramBotClient _bot;
        private readonly DialogueMemory _memory;
        private ICommand _currentState;
        private StateBehaviour _stateBehaviour;
        private string _chatId;

        public Bot(string token)
        {
            _memory = new DialogueMemory();
            _bot = new TelegramBotClient(token);

            var entryPoint = new StateBehaviourEntryPoint();
            _stateBehaviour = entryPoint.Initialize(token);
        }

        public void StartChating()
        {
            _bot.StartReceiving();
            _bot.OnMessage += ReceiveMessage;
            _bot.OnCallbackQuery += OnInlineButtonClick;
        }

        private void TryChangeCurrentCommand(ChatInfo info)
        {
            GetCurrentState(info.ChatId);

            try
            {
                _currentState = _stateBehaviour.StateMachine[info.LastMessage];
            }
            catch
            {
                if (_currentState == null)
                {
                    throw new IncorrectMessageException();
                }
            }

            SetCurrentState(info.ChatId);
        }

        public void DrawButtons(UI ui)
        {
            var keyboard = new List<List<InlineKeyboardButton>>();
            List<string> buttons = ui.ButtonCaptions;

            for (int i = 0; i < buttons.Count; i++)
            {
                var keyBoardButtons = new List<InlineKeyboardButton>();

                keyBoardButtons.Add(InlineKeyboardButton.WithCallbackData(buttons[i]));

                keyboard.Add(keyBoardButtons);
            }

            SendAnswer(ui, new InlineKeyboardMarkup(keyboard));
        }

        private void ReceiveMessage(object sender, MessageEventArgs arguments)
        {
            _chatId = arguments.Message.Chat.Id.ToString();
            string message = arguments.Message.Text;
            var info = new ChatInfo(_chatId, message);

            GetAnswer(info);
        }

        private void GetCurrentState(string chatId)
        {
            _currentState = _memory.Get(_chatId);
        }

        private void SetCurrentState(string chatId)
        {
            _memory.Set(_chatId, ref _currentState);
        }

        private void OnInlineButtonClick(object sc, CallbackQueryEventArgs ev)
        {
            InlineKeyboardMarkup keyboard = ev.CallbackQuery.Message.ReplyMarkup;

            _bot.EditMessageReplyMarkupAsync(ev.CallbackQuery.Message.Chat.Id, ev.CallbackQuery.Message.MessageId, keyboard);

            _chatId = ev.CallbackQuery.Message.Chat.Id.ToString();
            string message = ev.CallbackQuery.Data;

            var info = new ChatInfo(_chatId, message);

            GetAnswer(info);
        }

        private void GetAnswer(ChatInfo info)
        {
            UI ui;

            try
            {
                TryChangeCurrentCommand(info);

                ui = _currentState.Execute(info);

                SetCurrentState(info.ChatId);
            }
            catch (MessageException exc)
            {
                ui = exc.UI;
            }

            if (ui.ButtonCaptions != null)
            {
                DrawButtons(ui);
            }
            else
            {
                SendAnswer(ui);
            }
        }

        private void SendAnswer(UI ui, InlineKeyboardMarkup keyboard = null)
        {
            _bot.SendStickerAsync(_chatId, ui.StickerId);

            _bot.SendTextMessageAsync(_chatId, ui.Question, ParseMode.Markdown, 
                null, true, false, 0, false, keyboard);
        }
    }
}