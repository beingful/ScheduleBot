namespace PrimatScheduleBot
{
    public sealed class Stop : ICommand
    {
        public string Execute(string message, string chatId)
        {
            PostScheduler.TryStop(chatId);

            return "Ви відписалися від щоденної розсилки.";
        }
    }
}
