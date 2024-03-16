using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Events.Core.Interfaces;
using Events.Core.Notifications;
using Events.Domain.Enum;
using Events.Domain.Interfaces.Infra.Data;
using Events.Domain.Interfaces.Infra.Data.Repositories;
using Events.Domain.Models;
using Events.Domain.Models.Administracao;
using RabbitMQ.Client;

namespace Events.Domain.Commands.Evento
{
    public class EventoCommandHandler : CommandHandler, IRequestHandler<EventoCreateCommand>
       , IRequestHandler<EventoUpdateCommand>, IRequestHandler<EventoDeleteCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IEventoRepository _repository;

        public EventoCommandHandler(IEventoRepository repository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
        }

        public async Task<Unit> Handle(EventoCreateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Eventos evento = new Eventos(request.Descricao, request.Data);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                _repository.Add(evento);
                await Commit();
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, evento.Id, EnumTipoLog.CREATE, "Event", $"Event created: {request.Descricao}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.CREATE, "Event", "Error", notificationsString);
            }

            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "log",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "log",
                    basicProperties: null,
                    body: body);
            }

            return Unit.Value;
        }
        public async Task<Unit> Handle(EventoUpdateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Eventos evento = await _repository.GetById(request.Id);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                if (evento == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Update error: Non-existent event"));
                }
                else
                {
                    evento.SetUpdateEvento(request.Descricao, request.Data);
                    _repository.Update(evento);
                    await Commit();
                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, evento.Id, EnumTipoLog.UPDATE, "Event", $"Event updated: {request.Descricao}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.UPDATE, "Event", "Error", notificationsString);
            }

            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "log",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "log",
                    basicProperties: null,
                    body: body);
            }

            return Unit.Value;
        }

        public async Task<Unit> Handle(EventoDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Eventos evento = await _repository.GetById(request.IdEvento);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                if (evento == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Delete error: Non-existent event"));
                }
                else
                {
                    evento.SetExcluido(true);
                    _repository.Update(evento);
                    await Commit();
                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, evento.Id, EnumTipoLog.DELETE, "Event", $"Event deleted: {evento.Descricao}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.DELETE, "Event", "Error", notificationsString);
            }

            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "log",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer.Serialize(log);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "log",
                    basicProperties: null,
                    body: body);
            }

            return Unit.Value;
        }

    }
}
