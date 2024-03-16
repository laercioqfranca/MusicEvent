using Log.Core.Commands;
using Log.Core.Events;
using System.Threading.Tasks;

namespace Log.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
