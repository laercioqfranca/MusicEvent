using System.Text.Json;
using Subscriptions.Core.Events;
using Subscriptions.Core.Interfaces;

namespace Subscriptions.Infra.Data.EventSourcing
{
    public class EventStore : IEventStore
    {
        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(theEvent);
            var storedEvent = new StoredEvent(theEvent, serializedData, "_user.Name");
        }
    }
}
