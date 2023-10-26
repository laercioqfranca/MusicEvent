using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Domain.Enum;
using MusicEvent.Domain.Models.Autenticacao;

namespace MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth
{
    public interface IPerfilUsuarioRepository
    {
        Task<IEnumerable<PerfilUsuario>> GetPerfilUsuario(EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuario>> GetAll();
    }
}
