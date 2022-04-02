using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Stop : ICommand
    {
        private readonly UIBehaviour _ui;

        public Stop()
        {
            _ui = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Stop, new UI("Ви відписалися від щоденної розсилки.") }
            });
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            try
            {
                ui = _ui.StateMachine[info.LastMessage];
            }
            catch (KeyNotFoundException)
            {
                throw new IncorrectMessageException();
            }

            PostScheduler.TryStop(info.ChatId);

            return ui;
        }
    }
}
