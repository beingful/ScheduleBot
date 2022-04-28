using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class UIBehaviour
    {
        private readonly Dictionary<string, UI> StateMachine;

        public UIBehaviour(Dictionary<string, UI> stateMachine) => StateMachine = stateMachine;

        public UI GetUI(string key) => StateMachine[key];

        public bool IsSuchAKeyExist(string key) => StateMachine.ContainsKey(key);
    }
}
