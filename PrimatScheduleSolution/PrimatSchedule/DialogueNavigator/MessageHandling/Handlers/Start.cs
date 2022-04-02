using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public sealed class Start : ICommand
    {
        private readonly string _token;
        private readonly UIBehaviour _ui;
        private ICommand _currentCommand;

        public Start(string token)
        {
            _token = token;
            _ui = new UIBehaviour(new Dictionary<string, UI>
            {
                { Buttons.Start, new UI("О котрій годині ви хочете отримувати сповіщення?") }
            });
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (PostScheduler.IsSuchAJobExist(info.ChatId).Result)
            {
                throw new QuartzStartException();
            }

            try
            {
                ui = _ui.StateMachine[info.LastMessage];
            }
            catch
            {
                _currentCommand = new Mailing(info.LastMessage, _token);

                ui = _currentCommand.Execute(info);
            }

            return ui;
        }
    }
}
