using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Application.ViewModels.Auth;
using Events.Domain.Enum;

namespace Events.Application.Interfaces.Auth
{
    public interface IPerfilUsuarioAppService
    {
        Task<IEnumerable<PerfilUsuarioViewModel>> GetPerfilUsuario(Guid? idPerfil, EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuarioViewModel>> GetAll();
    }
}
