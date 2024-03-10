using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriptions.Domain.Enum;
using Subscriptions.Domain.Models;
using Subscriptions.Domain.Models.Autenticacao;

namespace Subscriptions.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IEventoRepository : IRepository<Evento>
    {
        Task<IEnumerable<Evento>> GetAll();
        Task<Evento> GetById(Guid idEvento);
    }
}
