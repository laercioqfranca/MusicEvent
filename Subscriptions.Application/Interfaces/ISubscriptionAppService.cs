using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Subscriptions.Application.DTO;

namespace Subscriptions.Application.Interfaces
{
    public interface ISubscriptionAppService : IDisposable
    {
        Task Create(InscricaoDTO inscricaoDTO);

    }
}
