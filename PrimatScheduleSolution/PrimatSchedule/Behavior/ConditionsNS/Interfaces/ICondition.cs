using System.Threading.Tasks;

namespace PrimatScheduleBot.Behavior.Interfaces;

internal interface ICondition
{
    public Task<bool> IsTrueAsync();
}
