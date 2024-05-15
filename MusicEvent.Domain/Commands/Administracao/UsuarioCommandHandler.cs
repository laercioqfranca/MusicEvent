using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text;
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
using RabbitMQ.Client;

namespace MusicEvent.Domain.Commands.Administracao
{
    public class UsuarioCommandHandler : CommandHandler, IRequestHandler<UsuarioCreateCommand>
       , IRequestHandler<UsuarioDeleteCommand>, IRequestHandler<UsuarioUpdateCommand>
    {

        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUsuarioRepository _repository;

        public UsuarioCommandHandler(IUsuarioRepository repository,
            IMediatorHandler bus,
            IUnitOfWork uow,
            INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
            _repository = repository;
        }

        public async Task<Unit> Handle(UsuarioCreateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            var query = await _repository.GetByLogin(request.Email);
            Usuario usuario = new Usuario();
            Guid idUsuario = Guid.NewGuid();

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                if (query.Count() > 0)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Usuário já existente!"));
                else
                {
                    usuario.setUsuario(idUsuario, request.Nome,request.Idade, request.Senha, request.Email, request.IdPerfil);
                    _repository.Add(usuario);
                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, usuario.Id, EnumTipoLog.CREATE, "Usuario", $"User created: {usuario.Nome}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.CREATE, "Usuario", "Error", notificationsString);
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

            if (_notifications.HasNotifications()) await Commit(true);
            if (!_notifications.HasNotifications()) await Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UsuarioUpdateCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Usuario usuario = await _repository.GetById(request.Id);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {

                if (usuario == null)
                    await _bus.RaiseEvent(new DomainNotification(request.MessageType, "Non-existent user!"));
                else
                {
                    usuario.setUpdateUsuario(request.Nome, request.Idade, request.Email, request.IdPerfil);
                    _repository.Update(usuario);
                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, usuario.Id, EnumTipoLog.UPDATE, "Usuario", $"Usur updated: {usuario.Nome}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.UPDATE, "Usuario", "Error", notificationsString);
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

            if (_notifications.HasNotifications()) await Commit(true);
            if (!_notifications.HasNotifications()) await Commit();

            return Unit.Value;
        }

        public async Task<Unit> Handle(UsuarioDeleteCommand request, CancellationToken cancellationToken)
        {
            LogHistorico log = new LogHistorico();
            Usuario usuario = await _repository.GetById(request.IdUsuario);

            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {

                if (usuario == null)
                    await _bus.RaiseEvent(new DomainNotification("Exclusão negada!", "O usuário não existe no banco de dados!"));

                else
                {
                    usuario.setExcluido(true);
                    _repository.Update(usuario);

                }
            }

            var notificationsString = _notifications.HasNotifications() ? string.Join(";", _notifications.GetNotifications().Select(x => x.Value)) : null;

            if (notificationsString == null)
            {
                log = new LogHistorico(request.UsuarioRequerenteId, usuario.Id, EnumTipoLog.DELETE, "Usuario", $"User deleted: {usuario.Nome}");
            }
            else
            {
                log = log.SaveLogHistorico(EnumTipoLog.DELETE, "Usuario", "Error", notificationsString);
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

            if (_notifications.HasNotifications()) await Commit(true);
            if (!_notifications.HasNotifications()) await Commit();

            return Unit.Value;
        }

    }
}
