using System.Collections.Generic;

namespace PrimatScheduleBot
{
    sealed class Help : Command
    {
        public Help() :
            base(new Dictionary<MessageResult, string>
            {
                {
                MessageResult.OK, $"Надішліть {PrimatScheduleBot.Messages.Start}, щоб підписатися на щоденну розсилку розкладу о 8:30 ранку.\n\n" +
                    $"Надішліть {PrimatScheduleBot.Messages.Stop}, щоб відписатися від щоденної розсилки.\n\n" +
                    $"Надішліть дату поточного семестру в форматі ММ-ДД, щоб дізнатися розклад на цю дату."
                }
            })
        { }

        public override string DoTaskAndGetMessage() => Messages.GetValueOrDefault(MessageResult.OK);
    }
}
