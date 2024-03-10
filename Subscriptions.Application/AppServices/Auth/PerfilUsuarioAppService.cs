using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Subscriptions.Application.Interfaces.Auth;
using Subscriptions.Application.ViewModels.Auth;
using Subscriptions.Core.Interfaces;
using Subscriptions.Domain.Enum;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth;

namespace Subscriptions.Application.AppServices.Auth
{
    public class PerfilUsuarioAppService : IPerfilUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IPerfilUsuarioRepository _repository;

        public PerfilUsuarioAppService(IMapper mapper, IMediatorHandler bus, IPerfilUsuarioRepository repository)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
        }

        public async Task<IEnumerable<PerfilUsuarioViewModel>> GetAll()
        {
            var query = await _repository.GetAll();
            var list = _mapper.Map<List<PerfilUsuarioViewModel>>(query);
            return list;

        }

        public async Task<IEnumerable<PerfilUsuarioViewModel>> GetPerfilUsuario(Guid? idPerfil, EnumTipoPerfil? tipoPerfil)
        {
            List<PerfilUsuarioViewModel> perfil = new List<PerfilUsuarioViewModel>();

            var query = await _repository.GetPerfilUsuario(tipoPerfil);
            var list = _mapper.Map<List<PerfilUsuarioViewModel>>(query);
           
            return perfil;

        }

    }

}
