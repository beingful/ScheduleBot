using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class UI
    {
        public readonly string Question;
        public readonly string StickerId;
        public readonly List<string> ButtonCaptions;

        public UI(string question, List<string> buttonCaptions = null) 
        {
            Question = question;
            ButtonCaptions = buttonCaptions;
        }

        public UI(string question, string stickerId, List<string> buttonCaptions = null) : this(question, buttonCaptions)
        {
            StickerId = stickerId;
        }
    }
}
