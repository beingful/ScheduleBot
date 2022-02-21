namespace PrimatScheduleBot
{
    public sealed class Stop : ICommand
    {
        private readonly string _chatId;

        public Stop(string chatId) => _chatId = chatId;

        public string Execute(string message)
        {
            PostScheduler.TryStop(_chatId);

            return "Ви відписалися від щоденної розсилки.";
        }
    }
}
