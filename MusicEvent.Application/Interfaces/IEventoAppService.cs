using System.Collections.Generic;
using System.Threading.Tasks;
using MusicEvent.Application.ViewModels;

namespace MusicEvent.Application.Interfaces
{
    public interface IEventoAppService
    {
        Task<IEnumerable<EventoViewModel>> GetAll();
    }
}
