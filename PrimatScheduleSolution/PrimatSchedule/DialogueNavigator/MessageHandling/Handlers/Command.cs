namespace PrimatScheduleBot
{
    public class Command : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;
        public readonly StateBehaviour _stateBehaviour;
        private ICommand _currentState;

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

                ui = _currentState.Execute(info);
            }

            return ui;
        }

        private void TryChangeCurrentCommand(string message) 
            => _currentState = _stateBehaviour.TryChangeCurrentState(message, _currentState);
    }
}