using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class UIBehaviour
    {
        public readonly Dictionary<string, UI> StateMachine;

        public UIBehaviour(Dictionary<string, UI> stateMachine) => StateMachine = stateMachine;
    }
}
