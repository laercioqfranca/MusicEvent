using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Domain.Models;
using MusicEvent.Domain.Models.Autenticacao;

namespace MusicEvent.Domain.Interfaces.Infra.Data.Repositories
{
    public interface IInscricaoRepository : IRepository<Inscricao>
    {
        Task<IEnumerable<Inscricao>> GetAllById(Guid idUsuario);
        Task<Inscricao> GetById(Guid idUsuario, Guid idEvento);
    }
}
