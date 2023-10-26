using MusicEvent.Core.Commands;
using MusicEvent.Core.Events;
using System.Threading.Tasks;

namespace MusicEvent.Core.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
