using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Log.Core.Interfaces;
using Log.Core.Notifications;
using Log.Domain.Interfaces.Infra.Data;
using Log.Domain.Interfaces.Infra.Data.Repositories;
using Log.Domain.Models.Administracao;

namespace Log.Domain.Commands
{
    public class LogCommandHandler : CommandHandler, IRequestHandler<LogCreateCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly ILogHistoricoRepository _logHistoricoRepository;

        public LogCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            ILogHistoricoRepository logHistoricoRepository)
            : base(uow, bus, notifications)
        {
            _logHistoricoRepository = logHistoricoRepository;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        public async Task<Unit> Handle(LogCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                LogHistorico logHistorico = new LogHistorico(request.UsuarioId, request.EntidadeId, request.TipoLog, request.NomeEntidade, request.Descricao);
                _logHistoricoRepository.Add(logHistorico);

                await Commit();
            }
            return Unit.Value;
        }

    }
}
