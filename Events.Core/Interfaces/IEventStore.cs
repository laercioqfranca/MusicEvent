using Events.Core.Events;

namespace Events.Core.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
