using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subscriptions.Application.DTO;
using Subscriptions.Application.ViewModels;

namespace Subscriptions.Application.Interfaces
{
    public interface ISubscriptionAppService : IDisposable
    {
        Task<IEnumerable<EventoViewModel>> GetAllById(Guid id);
        Task Create(InscricaoDTO inscricaoDTO);
        Task Delete(Guid id);

    }
}
