﻿using System;
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
            if (!request.IsValid())
                NotifyValidationErrors(request);
            else
            {
                Models.Inscricao inscricao = new((Guid)request.UsuarioRequerenteId, request.IdEvento);
                _repository.Add(inscricao);
                
                await Commit();

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

                    string message = JsonSerializer.Serialize(inscricao);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "log",
                        basicProperties: null,
                        body: body);
                }
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
