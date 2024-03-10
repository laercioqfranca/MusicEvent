using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriptions.Domain.Enum;
using Subscriptions.Domain.Models.Autenticacao;

namespace Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth
{
    public interface IPerfilUsuarioRepository
    {
        Task<IEnumerable<PerfilUsuario>> GetPerfilUsuario(EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuario>> GetAll();
    }
}
