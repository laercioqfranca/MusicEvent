using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log.Domain.Models;

namespace Log.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IInscricaoRepository : IRepository<Inscricao>
    {
        Task<IEnumerable<Inscricao>> GetAllById(Guid idUsuario);
        Task<Inscricao> GetById(Guid idUsuario, Guid idEvento);
    }
}
