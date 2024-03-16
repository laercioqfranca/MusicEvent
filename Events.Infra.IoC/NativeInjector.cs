using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Events.Infra.Bus;
using Events.Core.Interfaces;
using Events.Core.Notifications;
using Events.Infra.Data.Configuration;
using Events.Domain.Interfaces.Infra.Data;
using Events.Infra.Data.EventSourcing;
using Events.Infra.Data.Context;
using Events.Domain.Interfaces.Infra.Data.Repositories.Auth;
using Events.Infra.Data.Repositories.Auth;
using Events.Domain.Commands.Administracao;
using Events.Application.AppServices.Administracao;
using Events.Application.Interfaces.Administracao;
using Events.Application.AppServices.Auth;
using Events.Application.Interfaces.Auth;
using Events.Domain.Commands.Auth;
using Events.Application.AppServices.Autenticacao;
using Events.Domain.Commands.Inscricao;
using Events.Application.Interfaces;
using Events.Application.AppServices;
using Events.Domain.Interfaces.Infra.Data.Repositories;
using Events.Infra.Data.Repositories;
using Events.Domain.Commands.Evento;

namespace Events.Infra.IoC
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

            services.AddScoped<IRequestHandler<SubscriptionCreateCommand, Unit>, SubscriptionCommandHandler>();
            services.AddScoped<IRequestHandler<SubscriptionDeleteCommand, Unit>, SubscriptionCommandHandler>();

            services.AddScoped<IRequestHandler<EventoCreateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoUpdateCommand, Unit>, EventoCommandHandler>();
            services.AddScoped<IRequestHandler<EventoDeleteCommand, Unit>, EventoCommandHandler>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStore, EventStore>();

            // Infra - Data
            services.AddDbContext<MusicEventContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

            // Infra - Service
        }
    }
}