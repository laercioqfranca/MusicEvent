using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subscriptions.Application.DTO;
using Subscriptions.Application.ViewModels;

namespace Subscriptions.Application.Interfaces
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
