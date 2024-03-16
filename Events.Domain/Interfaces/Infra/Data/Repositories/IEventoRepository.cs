using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Domain.Enum;
using Events.Domain.Models;
using Events.Domain.Models.Autenticacao;

namespace Events.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IEventoRepository : IRepository<Eventos>
    {
        Task<IEnumerable<Eventos>> GetAll();
        Task<Eventos> GetById(Guid idEvento);
    }
}
