using Quartz;
using System.Threading.Tasks;

namespace PrimatScheduleBot
{
    public class DataActualisator : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var facade = new EventFacade();

            facade.RemoveAllBeforeToday();

            facade.Dispose();
        }
    }
}
