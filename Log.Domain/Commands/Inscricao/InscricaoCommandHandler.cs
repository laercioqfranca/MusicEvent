using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Log.Core.Interfaces;
using Log.Core.Notifications;
using Log.Domain.Interfaces.Infra.Data;
using Log.Domain.Interfaces.Infra.Data.Repositories;

namespace Log.Domain.Commands.Inscricao
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
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Inscricao inscricao = new(request.IdUsuario, request.IdEvento);
                _repository.Add(inscricao);
                
                await Commit();
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(InscricaoDeleteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Inscricao inscricao = await _repository.GetById((Guid)request.UsuarioRequerenteId, request.IdEvento);
                _repository.Remove(inscricao);

                await Commit();
            }

            return Unit.Value;
        }

    }
}
