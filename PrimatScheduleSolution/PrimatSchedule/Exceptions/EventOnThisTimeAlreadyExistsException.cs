namespace PrimatScheduleBot
{
    public class EventDateTimeDuplicationException : MessageException
    {
        public EventDateTimeDuplicationException() : base(new UI("Подія в цей день і на цей час вже існує.", Stickers.Typing)) { }
    }
}