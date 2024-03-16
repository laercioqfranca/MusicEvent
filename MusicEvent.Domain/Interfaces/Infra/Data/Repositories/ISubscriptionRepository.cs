using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicEvent.Domain.Models;

namespace MusicEvent.Domain.Interfaces.Infra.Data.Repositories
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<IEnumerable<Subscription>> GetAllById(Guid idUsuario);
        Task<Subscription> GetById(Guid idUsuario, Guid idEvento);
    }
}
