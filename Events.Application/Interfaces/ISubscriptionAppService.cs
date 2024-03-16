using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Events.Application.DTO;
using Events.Application.ViewModels;

namespace Events.Application.Interfaces
{
    public interface ISubscriptionAppService : IDisposable
    {
        Task<IEnumerable<EventoViewModel>> GetAllById(Guid id);
        Task Create(SubscriptionDTO inscricaoDTO);
        Task Delete(Guid id);

    }
}
