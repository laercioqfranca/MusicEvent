using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicEvent.Application.DTO;
using MusicEvent.Application.ViewModels.Administracao;
using MusicEvent.Application.ViewModels.Auth;

namespace MusicEvent.Application.Interfaces.Administracao
{
    public interface IUsuarioAppService : IDisposable
    {
        Task<UsuarioViewModel> GetById(Guid id);
        Task<UsuarioViewModel> GetByLogin(string login);
        Task<IEnumerable<UsuarioViewModel>> GetByFiltro(ConsultarPorFiltroViewModel filtro);
        Task<IEnumerable<UsuarioViewModel>> GetAll();
        Task Create(UsuarioDTO usuarioDTO);
        Task Update(UsuarioViewModel model);
        Task Delete(Guid id);

    }
}
