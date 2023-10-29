using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;

namespace MusicEvent.Domain.Commands.Evento
{
    public class EventoCommandHandler : CommandHandler, IRequestHandler<EventoCreateCommand>
       , IRequestHandler<EventoDeleteCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IEventoRepository _repository;
        private readonly ILogHistoricoRepository _logHistoricoRepository;

        public EventoCommandHandler(IEventoRepository repository,
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

        public async Task<Unit> Handle(EventoCreateCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Evento evento = new(request.Descricao, request.Data);
                _repository.Add(evento);
                
                await Commit();
            }
            return Unit.Value;
        }

        public async Task<Unit> Handle(EventoDeleteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Evento evento = await _repository.GetById(request.IdEvento);
                _repository.Remove(evento);

                await Commit();
            }

            return Unit.Value;
        }

    }
}
