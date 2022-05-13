using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatScheduleBot
{
    public class EventGetter
    {
        private readonly Guid _eventId;

        public EventGetter(Guid eventId) => _eventId = eventId;

        public Event TryGetEvent()
        {
            using var facade = new EventFacade();

            CheckIfEventExist(facade);

            return facade.GetById(_eventId);
        }

        private void CheckIfEventExist(EventFacade facade)
        {
            bool doesExist = facade.DoesAnyEventExist(@event => @event.Id == _eventId);

            Validation.ValidateDoesEventExist(doesExist);
        }
    }
}
