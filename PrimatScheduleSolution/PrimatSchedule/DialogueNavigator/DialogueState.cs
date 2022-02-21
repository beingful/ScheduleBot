using System;

namespace PrimatScheduleBot
{
    public class DialogueState
    {
        public enum States
        {
            Default,
            First,
            Second,
            Third,
            Final
        }

        public States CurrentState { get; private set; } = States.Default;

        public void ChangeState(int statesCount)
        {
            int currentStateOrder = (int)CurrentState;

            if (currentStateOrder == statesCount - 1)
            {
                CurrentState = States.Final;
            }
            else
            {
                CurrentState = (States)Enum.GetValues(typeof(States)).GetValue((int)CurrentState + 1);
            }
        }
    }
}
