using System;
using System.Threading.Tasks;

namespace PrimatScheduleBot
{
    public sealed class Delete : ICommand
    {
        private readonly Guid _eventId;

        public Delete(Guid eventId) => _eventId = eventId;

        public UI Execute(ChatInfo info)
        {
            var result = new CommandResult();
            var removing = Task.Run(() => Querier.DeleteEvent(_eventId));

            int resultIndex = removing.Result;

            return result._results[resultIndex];
        }
    }
}
