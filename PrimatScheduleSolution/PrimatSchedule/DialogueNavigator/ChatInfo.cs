namespace PrimatScheduleBot
{
    public class ChatInfo
    {
        public readonly string ChatId;
        public readonly string LastMessage;

        public ChatInfo(string chatId, string message)
        {
            ChatId = chatId;
            LastMessage = message;
        }
    }
}
