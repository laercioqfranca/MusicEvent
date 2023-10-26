using MediatR;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Enum;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Domain.Models.Administracao;
using MusicEvent.Domain.Models.Autenticacao;
using MusicEvent.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace MusicEvent.Domain.Commands.Auth
{
    public class AutenticacaoCommandHandler : CommandHandler, IRequestHandler<AutenticarCommand>, IRequestHandler<AlterarSenhaCommand>, IRequestHandler<ResetSenhaCommand>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUsuarioRepository _repository;
        private readonly ILogHistoricoRepository _logHistoricoRepository;
        private readonly DomainNotificationHandler _notifications;

        public AutenticacaoCommandHandler(IUsuarioRepository repository, IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications,
            ILogHistoricoRepository logHistoricoRepository) : base(uow, bus, notifications)
        {
            _logHistoricoRepository = logHistoricoRepository;
            _repository = repository;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<Unit> Handle(AutenticarCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            //LogHistorico logHistorico = new LogHistorico();

            if (!request.IsValid()) NotifyValidationErrors(request);
            else
            {

                var usersByLogin = await _repository.GetByLogin(request.Login);
                Usuario userQuery = usersByLogin.FirstOrDefault();

                if (!usersByLogin.Any())
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Acesso Negado")); //O nome de usuário informado é inválido
                else if (usersByLogin.Count() > 1)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Acesso Negado")); //Existe mais de um usuário com o mesmo login: { request.Login.ToLower() }  
                else
                {

                    string requestSenha = GetHash(userQuery.Salt, request.Senha);

                    if (requestSenha != userQuery.Senha)
                    {
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Usuário ou senha incorretos"));
                    }

                }

                //Log Histórico
                LogHistorico logHistorico = new LogHistorico();
                var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                //LogHistorico logHistorico = log.SaveLogHistorico(userQuery == null ? new Guid() : userQuery.Id, userQuery == null ? new Guid() : userQuery.Id, (int)EnumTipoLog.LOGIN, "Usuario",
                //     $"{userQuery.Nome} Logou com Sucesso!", notificationsString != null ? $"{request.Login}: {notificationsString}" : notificationsString);

                if (userQuery != null)
                {
                    logHistorico = log.SaveLogHistorico(userQuery == null ? new Guid() : userQuery.Id, userQuery == null ? new Guid() : userQuery.Id, EnumTipoLog.LOGIN, "Usuario",
                     $"{userQuery.Nome} Logou com Sucesso!", notificationsString != null ? $"{request.UsuarioRequerenteId}: {notificationsString}" : notificationsString);
                }
                else
                {
                    logHistorico = log.SaveLogHistorico(new Guid(), new Guid(), EnumTipoLog.LOGIN, "Usuario",
                     "Erro", notificationsString != null ? $"{request.Login}: {notificationsString}" : notificationsString);
                }


                _logHistoricoRepository.Add(logHistorico);

                if (_notifications.HasNotifications()) await Commit(true);
                if (!_notifications.HasNotifications()) await Commit();

            }
            return Unit.Value;
        }


        public async Task<Unit> Handle(AlterarSenhaCommand request, CancellationToken cancellationToken)
        {

            var usuario = await _repository.GetById(request.IdUsuario);
            var userQuery = usuario.FirstOrDefault();

            if (!request.IsValid()) NotifyValidationErrors(request);
            if (usuario == null)
            {
                await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Usuario não encontrado"));

            }
            else
            {

                LogHistorico log = new LogHistorico();

                //Verifica se a senha atual não bate com a que está no banco
                string senhaatualcript = GetHash(userQuery.Salt, request.SenhaAtual);

                if (senhaatualcript != userQuery.Senha)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Senha atual incorreta"));
                }
                else
                {
                    //Verifica se a senha nova é igual a senha atual
                    string senhanovacript = GetHash(userQuery.Salt, request.SenhaNova);
                    if (senhanovacript == userQuery.Senha)
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"A nova senha não poderá ser igual a senha atual"));
                    else
                    {
                        //Atualiza a senha
                        userQuery.setRedefinirSenha(false);
                        userQuery.setCriptografia(request.SenhaNova, "");

                        _repository.Update(userQuery);
                        await Commit();
                    }
                }

                //LOG HISTÓRICO
                var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                LogHistorico logHistorico = log.SaveLogHistorico(userQuery == null ? new Guid() : userQuery.Id, userQuery == null ? new Guid() : userQuery.Id, EnumTipoLog.ALTERACAO, userQuery.Nome, $"{userQuery.Login} Alterou a Senha", notificationsString);

                _logHistoricoRepository.Add(logHistorico);

                if (_notifications.HasNotifications()) await Commit(true);
                if (!_notifications.HasNotifications()) await Commit();

            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(ResetSenhaCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            if (!request.IsValid()) NotifyValidationErrors(request);
            else
            {

                //busca o usuário pelo login
                var query = await _repository.GetByLogin(request.Login);
                var usuario = query.FirstOrDefault();


                if (usuario == null) { 
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Usuário Inexistente"));
                }
                else
                {
                    if (usuario.Email != request.Email)
                    {
                        await _bus.RaiseEvent(new DomainNotification(request.MessageType, "CPF ou E-mail incorreto!"));
                    }
                    else
                    {
                        //Atualiza a base
                        string senhagerada = request.SenhaGerada;
                        usuario.setCriptografia(senhagerada, "");
                        usuario.setRedefinirSenha(true);

                        _repository.Update(usuario);
                        try
                        {
                            //Envia o email
                            

                            await Commit();
                            //LOG HISTÓRICO
                            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                            LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), usuario.Id, EnumTipoLog.ALTERACAO, usuario.GetType().Name, $"Usuário {usuario.Login} solicitou nova Senha por Email", notificationsString);
                            _logHistoricoRepository.Add(logHistorico);

                            if (_notifications.HasNotifications()) await Commit(true);
                            if (!_notifications.HasNotifications()) await Commit();
                        }
                        catch (Exception ex)
                        {
                            await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Servidor Indisponível - " + ex.Message));
                        }

                    }
                }

            }
            return Unit.Value;
        }

        private string GetSalt()
        {
            var Number = new byte[32];
            var Generator = RandomNumberGenerator.Create();
            Generator.GetBytes(Number);
            return Convert.ToBase64String(Number);
        }

        private static string GetHash(string Salt, string Password)
        {
            var SHA = SHA256.Create();
            var PasswordBytes = Encoding.UTF8.GetBytes(Salt + Password);
            var Hash = SHA.ComputeHash(PasswordBytes);
            return Convert.ToBase64String(Hash);
        }

    }
}
