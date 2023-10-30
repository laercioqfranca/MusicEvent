using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Enum;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Administracao;
using MusicEvent.Domain.Models.Autenticacao;
using MusicEvent.Domain.Utils;

namespace MusicEvent.Domain.Commands.Administracao
{
    public class UsuarioCommandHandler : CommandHandler, IRequestHandler<UsuarioCreateCommand>
       , IRequestHandler<UsuarioDeleteCommand>, IRequestHandler<UsuarioUpdateCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUsuarioRepository _repository;
        private readonly ILogHistoricoRepository _logHistoricoRepository;

        public UsuarioCommandHandler(IUsuarioRepository repository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            ILogHistoricoRepository logHistoricoRepository)
            : base(uow, bus, notifications)
        {
            _logHistoricoRepository = logHistoricoRepository;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
        }

        public async Task<Unit> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                var query = await _repository.GetByLogin(request.Email);

                if (query.Count() > 0)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Usuário já existente!"));
                else
                {
                    Usuario usuario = new Usuario();
                    Guid idUsuario = Guid.NewGuid();

                    usuario.setUsuario(idUsuario, request.Nome,request.Idade, request.Senha, request.Email, request.IdPerfil);

                    _repository.Add(usuario);

                    try
                    {

                        await Commit();
                        //Log de CREATE
                        var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                        LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), usuario.Id, EnumTipoLog.CRIACAO, usuario.GetType().Name, $"Usuário {usuario.Login} foi criado", notificationsString);
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
            return Unit.Value;
        }


        public async Task<Unit> Handle(UsuarioUpdateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                var query = await _repository.GetById(request.Id);
                Usuario usuario = query.FirstOrDefault();

                if (usuario == null)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Registro não existe!"));

                else
                {
                    usuario.setUpdateUsuario(request.Nome, request.Idade, request.Email, request.IdPerfil);

                    _repository.Update(usuario);
                    await Commit();

                    //Log de UPDATE
                    var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                    LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), usuario.Id, EnumTipoLog.ALTERACAO, usuario.GetType().Name, $"Usuário {usuario.Email} foi alterado", notificationsString);
                    _logHistoricoRepository.Add(logHistorico);

                    if (_notifications.HasNotifications()) await Commit(true);
                    if (!_notifications.HasNotifications()) await Commit();
                }
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(UsuarioDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                var query = await _repository.GetById(request.IdUsuario);
                Usuario usuario = query.FirstOrDefault();

                if (usuario == null)
                    await _bus.RaiseEvent(new DomainNotification("Exclusão negada!", "O usuário não existe no banco de dados!"));

                else
                {
                    usuario.setExcluido(true);
                    _repository.Update(usuario);

                    await Commit();

                    //Log de DELETE
                    var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                    LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), usuario.Id, EnumTipoLog.EXCLUSAO, usuario.GetType().Name, $"Usuário {usuario.Login} foi excluido logicamente", notificationsString);
                    _logHistoricoRepository.Add(logHistorico);

                    if (_notifications.HasNotifications()) await Commit(true);
                    if (!_notifications.HasNotifications()) await Commit();
                }
            }

            return Unit.Value;
        }

    }
}
