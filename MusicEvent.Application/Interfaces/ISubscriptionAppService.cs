using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicEvent.Application.DTO;
using MusicEvent.Application.ViewModels;

namespace MusicEvent.Application.Interfaces
{
    public interface ISubscriptionAppService : IDisposable
    {
        Task<IEnumerable<EventoViewModel>> GetAllById(Guid id);
        Task Create(InscricaoDTO inscricaoDTO);
        Task Delete(Guid id);

    }
}
