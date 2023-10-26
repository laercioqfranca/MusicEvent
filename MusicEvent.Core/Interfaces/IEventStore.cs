using MusicEvent.Core.Events;

namespace MusicEvent.Core.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
