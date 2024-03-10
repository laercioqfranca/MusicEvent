using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriptions.Application.ViewModels.Auth;
using Subscriptions.Domain.Enum;

namespace Subscriptions.Application.Interfaces.Auth
{
    public interface IPerfilUsuarioAppService
    {
        Task<IEnumerable<PerfilUsuarioViewModel>> GetPerfilUsuario(Guid? idPerfil, EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuarioViewModel>> GetAll();
    }
}
