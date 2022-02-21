namespace PrimatScheduleBot
{
    public class QuartzStopException : MessageException
    {
        private const string _message = "Ви ще не підписані на щоденну розсилку розкладу.";
        public QuartzStopException() : base(_message) { }
    }
}
