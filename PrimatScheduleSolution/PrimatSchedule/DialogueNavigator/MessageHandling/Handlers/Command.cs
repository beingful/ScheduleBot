namespace PrimatScheduleBot
{
    public class Command : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        private readonly StateBehaviour _stateBehaviour;

        public Command(UIBehaviour uiBehaviour, StateBehaviour stateBehaviour)
        {
            _uiBehaviour = uiBehaviour;
            _stateBehaviour = stateBehaviour;
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            if (_uiBehaviour.IsSuchAKeyExist(info.LastMessage))
            {
                ui = _uiBehaviour.GetUI(info.LastMessage);
            }
            else
            {
                TryChangeCurrentCommand(info.LastMessage);

                ui = _stateBehaviour.CurrentState.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(string message) 
            => _stateBehaviour.TryChangeCurrentState(message);
    }
}