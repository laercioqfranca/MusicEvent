using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Domain.Enum;
using Events.Domain.Models.Autenticacao;

namespace Events.Domain.Interfaces.Infra.Data.Repositories.Auth
{
    public interface IPerfilUsuarioRepository
    {
        Task<IEnumerable<PerfilUsuario>> GetPerfilUsuario(EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuario>> GetAll();
    }
}
