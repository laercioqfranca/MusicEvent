using Events.Core.Commands;
using Events.Core.Events;
using System.Threading.Tasks;

namespace Events.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
