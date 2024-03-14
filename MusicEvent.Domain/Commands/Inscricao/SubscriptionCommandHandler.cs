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
            Models.Subscription inscricao = null;

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Usuario usuario = await _usuarioRepository.GetById((Guid)request.UsuarioRequerenteId);

                if (usuario == null)
                {
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, $"Non-existent user"));
                }
                else
                {

                    inscricao = new((Guid)request.UsuarioRequerenteId, request.IdEvento);

                    _repository.Add(inscricao);
                
                    await Commit();

                }

            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(inscricao.IdUsuario, inscricao.IdEvento, EnumTipoLog.CRIACAO, "Subscription", "User subscribed");
            }
            else
            {
                //log = new LogHistorico(null, null, EnumTipoLog.CRIACAO, "Subscription", "User subscribed");
                log = log.SaveLogHistorico(EnumTipoLog.CRIACAO, "Subscription", "Error", notificationsString);
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
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Subscription inscricao = await _repository.GetById((Guid)request.UsuarioRequerenteId, request.IdEvento);
                _repository.Remove(inscricao);

                await Commit();
            }

            return Unit.Value;
        }

    }
}
