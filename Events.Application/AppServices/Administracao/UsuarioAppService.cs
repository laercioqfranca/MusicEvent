using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Events.Application.Interfaces.Administracao;
using Events.Application.ViewModels.Administracao;
using Events.Core.Interfaces;
using Events.Domain.Commands.Administracao;
using Events.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Events.Application.ViewModels.Auth;
using Events.Application.DTO;

namespace Events.Application.AppServices.Administracao
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public UsuarioAppService(IMapper mapper, IMediatorHandler bus, IUsuarioRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _bus = bus;
            _repository = repository;
            _httpContextAcessor = httpContextAccessor;
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetAll()
        {
            var usuarios = await _repository.GetAll();
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(usuarios);
        }

        public async Task<UsuarioViewModel> GetById(Guid id)
        {
            var query = await _repository.GetById(id);
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(query).FirstOrDefault();
        }

        public async Task<UsuarioViewModel> GetByLogin(string login)
        {
            var query = await _repository.GetByLogin(login);
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(query).FirstOrDefault();
        }

        public async Task<IEnumerable<UsuarioViewModel>> GetByFiltro(ConsultarPorFiltroViewModel filtro)
        {
            var usuarios = await _repository.GetByFiltro(filtro.Nome, filtro.CPF, filtro.Email);
            return _mapper.ProjectTo<UsuarioViewModel>(usuarios.AsQueryable());
        }

        public async Task Create(UsuarioDTO usuarioDTO)
        {
            var command = _mapper.Map<UsuarioCreateCommand>(usuarioDTO);
            await _bus.SendCommand(command);
        }

        public async Task Update(UsuarioViewModel model)
        {
            var command = _mapper.Map<UsuarioUpdateCommand>(model);
            await _bus.SendCommand(command);
        }

        public async Task Delete(Guid id)
        {
            var command = new UsuarioDeleteCommand(id);
            await _bus.SendCommand(command);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
