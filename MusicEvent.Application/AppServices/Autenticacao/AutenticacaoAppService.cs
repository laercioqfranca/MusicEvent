using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MusicEvent.Application.Interfaces.Auth;
using MusicEvent.Application.ViewModels.Auth;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Commands.Auth;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Autenticacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Application.AppServices.Autenticacao
{
    public class AutenticacaoAppService : IAutenticacaoAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUsuarioRepository _repository;
        private readonly IHttpContextAccessor _httpContextAcessor;

        public AutenticacaoAppService(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications, IMapper mapper, IUsuarioRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _bus = bus;
            _mapper = mapper;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _httpContextAcessor = httpContextAccessor;
        }

        public async Task<UsuarioViewModel> Autenticar(LoginViewModel loginViewModel)
        {
            UsuarioViewModel userViewModel = null;
            var command = _mapper.Map<AutenticarCommand>(loginViewModel);
            await _bus.SendCommand(command);
            if (!_notifications.HasNotifications())
            {
                var usuario = (await _repository.GetByLogin(loginViewModel.Login)).FirstOrDefault();

                userViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            }
            return userViewModel;
        }

        public async Task AlterarSenha(AlterarSenhaUsuarioViewModel commandAlterarSenha)
        {
            var command = _mapper.Map<AlterarSenhaCommand>(commandAlterarSenha);
            //command.UsuarioRequerenteId = Guid.Parse(_httpContextAcessor.HttpContext.User.Identity.Name);
            await _bus.SendCommand(command);
        }

        public async Task ResetarSenhaPorEmail(ResetSenhaViewModel resetPasswordViewModel)
        {
            var command = _mapper.Map<ResetSenhaCommand>(resetPasswordViewModel);
            await _bus.SendCommand(command);

        }

        public string GerarNovaSenha()
        {
            Usuario usuario = new Usuario();

            return usuario.setGerarSenhaAleatoria();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
