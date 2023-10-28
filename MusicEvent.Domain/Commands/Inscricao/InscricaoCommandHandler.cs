using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Administracao;

namespace MusicEvent.Domain.Commands.Inscricao
{
    public class InscricaoCommandHandler : CommandHandler, IRequestHandler<InscricaoCreateCommand>
       , IRequestHandler<InscricaoDeleteCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IInscricaoRepository _repository;
        private readonly ILogHistoricoRepository _logHistoricoRepository;

        public InscricaoCommandHandler(IInscricaoRepository repository,
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

        public async Task<Unit> Handle(InscricaoCreateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Inscricao inscricao = new((Guid)request.UsuarioRequerenteId, request.IdEvento);
                _repository.Add(inscricao);
                
                await Commit();

                ////Log de CREATE
                //var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                //LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), request.IdUsuario, EnumTipoLog.CRIACAO, "", $"O Evento {request.IdEvento} foi criado para o usuário {request.IdUsuario}", notificationsString);
                //_logHistoricoRepository.Add(logHistorico);

                //if (_notifications.HasNotifications()) await Commit(true);
                //if (!_notifications.HasNotifications()) await Commit();
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(InscricaoDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Inscricao inscricao = await _repository.GetById((Guid)request.UsuarioRequerenteId, request.IdEvento);
                _repository.Remove(inscricao);

                await Commit();

                //    //Log de DELETE
                //    var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

                //    LogHistorico logHistorico = log.SaveLogHistorico(request.UsuarioRequerenteId.ToGuid(), usuario.Id, EnumTipoLog.EXCLUSAO, usuario.GetType().Name, $"Usuário {usuario.Login} foi excluido logicamente", notificationsString);
                //    _logHistoricoRepository.Add(logHistorico);

                //    if (_notifications.HasNotifications()) await Commit(true);
                //    if (!_notifications.HasNotifications()) await Commit();
            }

            return Unit.Value;
        }

    }
}
