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
using Microsoft.Extensions.Configuration;
using MassTransit;
using System.Collections.Generic;

namespace MusicEvent.Domain.Commands.Inscricao
{
    public class SubscriptionCommandHandler : CommandHandler, IRequestHandler<SubscriptionCreateCommand>
       , IRequestHandler<SubscriptionDeleteCommand>
    {

        private readonly IMediatorHandler _mediatorHandler;
        private readonly DomainNotificationHandler _notifications;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public SubscriptionCommandHandler(
            ISubscriptionRepository subscriptionRepository,
            IMediatorHandler mediatorHandler,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications,
            IUsuarioRepository usuarioRepository,
            IBus bus,
            IConfiguration configuration)
            : base(uow, mediatorHandler, notifications)
        {
            _mediatorHandler = mediatorHandler;
            _notifications = (DomainNotificationHandler)notifications;
            _subscriptionRepository = subscriptionRepository;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _bus = bus;
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
                Subscription subscriptionExists = await _subscriptionRepository.GetById(usuario.Id, request.IdEvento);
               

                if (usuario == null)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification(request.MessageType, $"Create error: Non-existent user"));
                }
                else if(subscriptionExists != null)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification(request.MessageType, $"Você já está inscrito nesse evento"));
                }
                else
                {

                    subscription = new((Guid)request.UsuarioRequerenteId, request.IdEvento);

                    _subscriptionRepository.Add(subscription);
                
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

            //var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            //using var connection = factory.CreateConnection();
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(
            //        queue: "log",
            //        durable: false,
            //        exclusive: false,
            //        autoDelete: false,
            //        arguments: null);

            //    string message = JsonSerializer.Serialize(log);
            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(
            //        exchange: "",
            //        routingKey: "log",
            //        basicProperties: null,
            //    body: body);
            //}

            var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));

            await endpoint.Send(new LogHistorico(subscription.IdUsuario, subscription.IdEvento, EnumTipoLog.CREATE, "Subscription", "User subscribed"));

            return Unit.Value;
        }

        public async Task<Unit> Handle(SubscriptionDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Subscription subscription = await _subscriptionRepository.GetById((Guid)request.UsuarioRequerenteId, request.IdEvento); ;

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                if(subscription == null)
                {
                    await _mediatorHandler.RaiseEvent(new DomainNotification(request.MessageType, $"Delete error: Non-existent subscription"));
                }
                else
                {
                    _subscriptionRepository.Remove(subscription);
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

            //var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            //using var connection = factory.CreateConnection();
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(
            //        queue: "log",
            //        durable: false,
            //        exclusive: false,
            //        autoDelete: false,
            //        arguments: null);

            //    string message = JsonSerializer.Serialize(log);
            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(
            //        exchange: "",
            //        routingKey: "log",
            //        basicProperties: null,
            //        body: body);
            //}

            return Unit.Value;
        }

    }
}
