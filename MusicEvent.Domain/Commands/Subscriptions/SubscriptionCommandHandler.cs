using System;
using System.Text.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Domain.Interfaces.Infra.Data;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories.Auth;
using MusicEvent.Domain.Models.Administracao;
using RabbitMQ.Client;
using MusicEvent.Domain.Models.Autenticacao;
using System.Linq;
using MusicEvent.Domain.Enum;
using MusicEvent.Domain.Models;

namespace MusicEvent.Domain.Commands.Inscricao
{
    public class SubscriptionCommandHandler : CommandHandler, IRequestHandler<SubscriptionCreateCommand>
       , IRequestHandler<SubscriptionDeleteCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly ISubscriptionRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SubscriptionCommandHandler(ISubscriptionRepository repository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IUsuarioRepository usuarioRepository)
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Unit> Handle(SubscriptionCreateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Subscription subscription = null;

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Usuario usuario = await _usuarioRepository.GetById((Guid)request.UsuarioRequerenteId);

                if (usuario == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Create error: Non-existent user"));
                }
                else
                {

                    subscription = new((Guid)request.UsuarioRequerenteId, request.IdEvento);

                    _repository.Add(subscription);
                
                    await Commit();

                }

            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(subscription.IdUsuario, subscription.IdEvento, EnumTipoLog.CREATE, "Subscription", "User subscribed");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.CREATE, "Subscription", "Error", notificationsString);
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

        public async Task<Unit> Handle(SubscriptionDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Subscription subscription = await _repository.GetById((Guid)request.UsuarioRequerenteId, request.IdEvento); ;

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                if(subscription == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Delete error: Non-existent subscription"));
                }
                else
                {
                    _repository.Remove(subscription);
                    await Commit();
                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(subscription.IdUsuario, subscription.IdEvento, EnumTipoLog.DELETE, "Subscription", "User unsubscribed");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.DELETE, "Subscription", "Error", notificationsString);
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
