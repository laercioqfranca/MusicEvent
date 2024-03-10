using Subscriptions.Core.Commands;
using Subscriptions.Core.Events;
using System.Threading.Tasks;

namespace Subscriptions.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
