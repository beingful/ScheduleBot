namespace PrimatScheduleBot
{
    public sealed class Help : ICommand
    {
        public string Execute(string message)
        {
            return $"Надішліть {Messages.Start}, щоб підписатися на щоденну розсилку розкладу о 8:30 ранку.\n\n" +
                    $"Надішліть {Messages.Stop}, щоб відписатися від щоденної розсилки.\n\n" +
                    $"Надішліть дату поточного семестру в форматі ММ-ДД, щоб дізнатися розклад на цю дату.";
        }
    }
}
