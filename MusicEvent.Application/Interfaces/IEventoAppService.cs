using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicEvent.Application.DTO;
using MusicEvent.Application.ViewModels;

namespace MusicEvent.Application.Interfaces
{
    public interface IEventoAppService
    {
        Task<IEnumerable<EventoViewModel>> GetAll();
        Task<EventoViewModel> GetById(Guid id);
        Task Create(EventoDTO eventoDTO);
        Task Update(EventoDTO eventoDTO);
        Task Delete(Guid id);
    }
}
