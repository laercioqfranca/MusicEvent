using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Domain.Enum;
using MusicEvent.Domain.Models;
using MusicEvent.Domain.Models.Autenticacao;

namespace MusicEvent.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IEventoRepository : IRepository<Eventos>
    {
        Task<IEnumerable<Eventos>> GetAll();
        Task<Eventos> GetById(Guid idEvento);
    }
}
