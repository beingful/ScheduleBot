namespace PrimatScheduleBot
{
    public class Get : ICommand
    {
        private readonly Schedule _schedule;

        public Get(Schedule schedule) => _schedule = schedule;

        public UI Execute(ChatInfo info)
        {
            var parser = new ScheduleToMessage(_schedule);

            return new UI(parser.ToMessage(), Stickers.Walking);
        }
    }
}
