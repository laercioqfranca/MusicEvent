using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Domain.Enum;

namespace MusicEvent.Application.Interfaces.Auth
{
    public interface IPerfilUsuarioAppService
    {
        Task<IEnumerable<PerfilUsuarioViewModel>> GetPerfilUsuario(Guid? idPerfil, EnumTipoPerfil? tipoPerfil);
        Task<IEnumerable<PerfilUsuarioViewModel>> GetAll();
    }
}
