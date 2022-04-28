using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace PrimatScheduleBot
{
    public class Bot
    {
        private readonly TelegramBotClient _bot;
        private readonly MemorableStateBehaviour _stateBehaviour;
        private ICommand _currentState;

        public Bot(string token)
        {
            _bot = new TelegramBotClient(token);
            _stateBehaviour = StateBehaviourEntryPoint.Initialize(token);

            var schedules = new Schedules(token);
            schedules.Start();
        }

        public void StartChating()
        {
            _bot.StartReceiving();
            SubscribeToChat();
        }

        private void SubscribeToChat()
        {
            _bot.OnMessage += ReceiveMessage;
            _bot.OnCallbackQuery += OnInlineButtonClick;
        }

        private void ReceiveMessage(object sender, MessageEventArgs arguments)
        {
            Message message = arguments.Message;

            Answer(message.Text, message.Chat.Id.ToString());
        }

        private async void OnInlineButtonClick(object sc, CallbackQueryEventArgs ev)
        {
            CallbackQuery callback = ev.CallbackQuery;
            Message message = callback.Message;

            KeepMessageReplyMarkup(message.Chat.Id, message.MessageId, message.ReplyMarkup);
            
            Answer(callback.Data, message.Chat.Id.ToString());
        }

        private void KeepMessageReplyMarkup(long chatId, int messageId, InlineKeyboardMarkup keyboard) 
            => _bot.EditMessageReplyMarkupAsync(chatId, messageId, keyboard);

        private UI GetAnswer(ChatInfo info)
        {
            UI ui;

            try
            {
                _currentState = _stateBehaviour.TryChangeCurrentState(info.ChatId, info.LastMessage);

                ui = _currentState.Execute(info);

                _stateBehaviour.SaveCurrentCommandInCache(info.ChatId, _currentState);
            }
            catch (MessageException exc)
            {
                ui = exc.UI;
            }

            return ui;
        }

        private void Answer(string message, string chatId)
        {
            var info = new ChatInfo(chatId, message);

            UI ui = GetAnswer(info);

            InlineKeyboardMarkup keyboard = ui.GetInlineKeyboard();

            SendAnswer(info.ChatId, ui, keyboard);
        }

        private void SendAnswer(string chatId, UI ui, InlineKeyboardMarkup keyboard)
        {
            _bot.SendStickerAsync(chatId, ui.StickerId);
            _bot.SendTextMessageAsync(chatId, ui.Question, ParseMode.Markdown, null, true, false, 0, false, keyboard);
        }
    }
}