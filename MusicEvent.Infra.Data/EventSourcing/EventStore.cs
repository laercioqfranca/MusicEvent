using System.Text.Json;
using MusicEvent.Core.Events;
using MusicEvent.Core.Interfaces;

namespace MusicEvent.Infra.Data.EventSourcing
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
