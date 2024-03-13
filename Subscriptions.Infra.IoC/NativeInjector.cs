using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Subscriptions.Infra.Bus;
using Subscriptions.Core.Interfaces;
using Subscriptions.Core.Notifications;
using Subscriptions.Infra.Data.Configuration;
using Subscriptions.Domain.Interfaces.Infra.Data;
using Subscriptions.Infra.Data.EventSourcing;
using Subscriptions.Infra.Data.Context;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Subscriptions.Infra.Data.Repositories.Auth;
using Subscriptions.Domain.Commands.Administracao;
using Subscriptions.Application.AppServices.Administracao;
using Subscriptions.Application.Interfaces.Administracao;
using Subscriptions.Application.AppServices.Auth;
using Subscriptions.Application.Interfaces.Auth;
using Subscriptions.Infra.Data.Repositories.LogRepository;
using Subscriptions.Domain.Commands.Auth;
using Subscriptions.Application.AppServices.Autenticacao;
using Subscriptions.Domain.Commands.Inscricao;
using Subscriptions.Application.Interfaces;
using Subscriptions.Application.AppServices;
using Subscriptions.Domain.Interfaces.Infra.Data.Repositories;
using Subscriptions.Infra.Data.Repositories;
using Subscriptions.Domain.Commands.Evento;

namespace Subscriptions.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Application
            services.AddScoped<IAutenticacaoAppService, AutenticacaoAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IPerfilUsuarioAppService, PerfilUsuarioAppService>();
            services.AddScoped<IEventoAppService, EventoAppService>();
            services.AddScoped<ISubscriptionAppService, SubscriptionAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<AutenticarCommand, Unit>, AutenticacaoCommandHandler>();

            services.AddScoped<IRequestHandler<UsuarioCreateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioUpdateCommand, Unit>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<UsuarioDeleteCommand, Unit>, UsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<InscricaoCreateCommand, Unit>, InscricaoCommandHandler>();
            services.AddScoped<IRequestHandler<InscricaoDeleteCommand, Unit>, InscricaoCommandHandler>();

            services.AddScoped<IRequestHandler<EventoCreateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoUpdateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoDeleteCommand, Unit>, EventoCommandHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<ILogHistoricoRepository, LogHistoricoRepository>();

            // Infra - Data
            services.AddDbContext<SubscriptionsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IInscricaoRepository, InscricaoRepository>();

            // Infra - Service
        }
    }
}