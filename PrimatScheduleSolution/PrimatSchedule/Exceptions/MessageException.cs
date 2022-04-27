using System;

namespace PrimatScheduleBot
{
    public class MessageException : Exception
    {
        public UI UI { get; }

        public MessageException(UI ui) => UI = ui;
    }
}
