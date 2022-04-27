namespace PrimatScheduleBot
{
    public class PostSenderData
    {
        public readonly string Token;
        public readonly string ChatId;

        public PostSenderData(string token, string chatId)
        {
            Token = token;
            ChatId = chatId;
        }
    }
}
