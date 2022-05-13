using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Delete : ICommand
    {
        private readonly Guid _eventId;

        public Delete(Guid eventId) => _eventId = eventId;

        public UI Execute(ChatInfo info)
        {
            DeleteEvent();

            return new UI("Я видалив подію із розкладу.", Stickers.Done); ;
        }

        private Event TryGetEvent()
        {
            var getter = new EventGetter(_eventId);

            return getter.TryGetEvent();
        }

        private void DeleteEvent()
        {
            Event @event = TryGetEvent();

            using var facade = new EventFacade();

            facade.Remove(@event);
        }
    }
}