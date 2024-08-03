using PrimatScheduleBot.Behavior.Interfaces;
using PrimatScheduleBot.Behavior.States.Interfaces;

namespace PrimatScheduleBot.Behavior.Transitions;

internal record class Transition(IState From, IState To, ICondition If);
