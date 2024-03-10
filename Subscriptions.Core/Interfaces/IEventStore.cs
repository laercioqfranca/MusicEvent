using Subscriptions.Core.Events;

namespace Subscriptions.Core.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
